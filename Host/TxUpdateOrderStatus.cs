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
                return res;
            }

            res = ValidateOrderInfo(order);
            if(res!= Result.Success)
            {
                return res;
            }
            if (curReq.ToStatus == (int)OrderStatus.OrderRejected)
            {
                if (order.Status != (int)OrderStatus.OrderPending)
                {
                    return Result.Failed;
                }
            }
            if (curReq.ToStatus == (int)OrderStatus.OrderConfirmed)
            {
                if (order.Status != (int)OrderStatus.OrderPending)
                {
                    return Result.Failed;
                }
            }
            if(curReq.ToStatus > (int)OrderStatus.OrderConfirmed && !order.Paid)
            {
                return Result.Failed;
            }
            if (curReq.ToStatus == (int)OrderStatus.RawPhotoUploaded)
            {
                if (order.Status != (int)OrderStatus.OrderConfirmed)
                {
                    return Result.Failed;
                }
            }
            if (curReq.ToStatus == (int)OrderStatus.PhotoSelected)
            {
                if (order.Status != (int)OrderStatus.RawPhotoUploaded)
                {
                    return Result.Failed;
                }
            }
            if (curReq.ToStatus == (int)OrderStatus.RetouchedPhotoUploaded)
            {
                if (order.Status != (int)OrderStatus.PhotoSelected)
                {
                    return Result.Failed;
                }
            }
            if (curReq.ToStatus == (int)OrderStatus.OrderFinalised)
            {
                if (order.Status != (int)OrderStatus.RetouchedPhotoUploaded)
                {
                    return Result.Failed;
                }

                //Update PhotographerAccount
                PhotographerAccount pa = new PhotographerAccount();
                pa.PhotographerId = order.PhotographerId;
                pa = pa.Fetch() as PhotographerAccount;
                if(pa==null)
                {
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
