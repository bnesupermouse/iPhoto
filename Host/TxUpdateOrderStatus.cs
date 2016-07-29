using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Host;
using HostDB;
using HostMessage.Responses;
using Host.Common;
using Host.Models;
using System.IO.Compression;
using Microsoft.Owin.FileSystems;

namespace Host
{
    public class TxUpdateOrderStatus:Tx
    {
        public TxUpdateOrderStatus()
        {
            TxnId = 1;
        }

        public override Result Validate()
        {
            var curReq = request as UpdateOrderStatus;
            //Check Order
            CustomerOrder order = new CustomerOrder();
            order.SerialNo = curReq.OrderId;
            order = order.Fetch() as CustomerOrder;
            if (order == null)
            {
                LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Invalid Request", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Request";
                return Result.Failed;
            }
            PhotographerId = order.PhotographerId;
            CustomerId = order.CustomerId;
            //Check Session
            var res = Result.Success;
            if (curReq.ToStatus == (int)OrderStatus.PhotoSelected || curReq.ToStatus == (int)OrderStatus.OrderFinalised)
            {
                res = UpdateCustomerSession(true);
            }
            else if(curReq.ToStatus == (int)OrderStatus.OrderConfirmed || curReq.ToStatus == (int)OrderStatus.RawPhotoUploaded || curReq.ToStatus == (int)OrderStatus.RetouchedPhotoUploaded)
            {
                res = UpdatePhotographerSession(true);
            }
            if (res != Result.Success)
            {
                LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Invalid Status Value", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Status Value";
                return res;
            }

            res = ValidateOrderInfo(order);
            if(res!= Result.Success)
            {
                LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Order Info Check Failed", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Request";
                return res;
            }
            if (curReq.ToStatus == (int)OrderStatus.OrderRejected)
            {
                if (order.Status != (int)OrderStatus.OrderPending)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Order Cannot be rejected", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Order Cannot be rejected";
                    return Result.Failed;
                }
            }
            if (curReq.ToStatus == (int)OrderStatus.OrderConfirmed)
            {
                if (order.Status != (int)OrderStatus.OrderPending)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Order Cannot be confirmed", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Order Cannot be confirmed";
                    return Result.Failed;
                }
            }
            if(curReq.ToStatus != (int)OrderStatus.OrderRejected && curReq.ToStatus > (int)OrderStatus.OrderConfirmed && !order.Paid)
            {
                LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Order is not Paid", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Order is not paid";
                return Result.Failed;
            }
            if (curReq.ToStatus == (int)OrderStatus.RawPhotoUploaded)
            {
                if (order.Status != (int)OrderStatus.OrderConfirmed)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Order is not Confirmed", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Order is not Confirmed";
                    return Result.Failed;
                }
            }
            if (curReq.ToStatus == (int)OrderStatus.PhotoSelected)
            {
                if (order.Status != (int)OrderStatus.RawPhotoUploaded)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Raw Photo is not Uploaded", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Raw Photo is not Uploaded";
                    return Result.Failed;
                }
            }
            if (curReq.ToStatus == (int)OrderStatus.RetouchedPhotoUploaded)
            {
                if (order.Status != (int)OrderStatus.PhotoSelected)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Raw Photo is not Selected", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Raw Photo is not Selected";
                    return Result.Failed;
                }
            }
            if (curReq.ToStatus == (int)OrderStatus.OrderFinalised)
            {
                if (order.Status != (int)OrderStatus.RetouchedPhotoUploaded)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Retouched Photo is not Uploaded", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Retouched Photo is not Uploaded";
                    return Result.Failed;
                }

                //Update PhotographerAccount
                PhotographerAccount pa = new PhotographerAccount();
                pa.PhotographerId = order.PhotographerId;
                pa = pa.Fetch() as PhotographerAccount;
                if(pa==null)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Could not find Photographer", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Could not find Photographer";
                    return Result.Failed;
                }
                var newPa = pa.Clone() as PhotographerAccount;
                newPa.Balance += order.PhotographerPay.Value;
                newPa.PendingBalance -= order.PhotographerPay.Value;
                Data.AddNew(pa, newPa);

                //Update Account
                Account acc = new Account();
                acc.AccountId = 1;
                acc = acc.Fetch() as Account;
                if (acc == null)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Could not find Account", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Could not find Account";
                    return Result.Failed;
                }
                else
                {
                    var newAcc = acc.Clone() as Account;
                    newAcc.PendingPay -= order.PhotographerPay.Value;
                    Data.AddNew(acc, newAcc);
                }
            }
            //Construct new Order
            CustomerOrder newOrder = order.Clone() as CustomerOrder;
            newOrder.Status = (int)curReq.ToStatus;

            Data.AddNew(order, newOrder);
            var resp = new UpdateOrderStatusResponse();
            resp.OrderId = newOrder.SerialNo;
            resp.Status = curReq.ToStatus;
            resp.StatusString = StatusValue.GetStatusValue(newOrder.Status, newOrder.Paid);
            resp.LabelString = StatusValue.GetLabelValue(newOrder.Status, newOrder.Paid);
            response = resp;

            if(curReq.ToStatus == (int)OrderStatus.OrderFinalised)
            {
                var rootPath = new PhysicalFileSystem(@"../../WebSrc/app");
                long OrderId = newOrder.SerialNo;

                string rawPhotoPath = rootPath.Root + @"/images/customer/" + OrderId + @"/" + @"raw";
                string rawZipFile = rawPhotoPath + @"/" + OrderId + @"_Raw.zip";
                string retouchedPhotoPath = rootPath.Root + @"/images/customer/" + OrderId + @"/" + @"retouched";
                string retouchedZipFile = retouchedPhotoPath + @"/" + OrderId + @"_Retouched.zip";
                try
                {
                    ZipFile.CreateFromDirectory(rawPhotoPath, rawZipFile);
                    ZipFile.CreateFromDirectory(retouchedPhotoPath, retouchedZipFile);
                }
                catch(Exception ex)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Failed to Archive photos", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Failed to Archive photos";
                    return Result.Failed;
                }
            }
            return Result.Success;
        }
        public override Result Prepare()
        {
            return Data.Validate();
        }
        public override Result Update()
        {
            return Data.Update();
        }
    }
}
