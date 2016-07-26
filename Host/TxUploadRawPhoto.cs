using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Host;
using HostDB;
using HostMessage.Responses;
using Host.Common;

namespace Host
{
    public class TxUploadRawPhoto : Tx
    {
        public TxUploadRawPhoto()
        {
            TxnId = 1;
        }

        public override Result Validate()
        {
            var curReq = request as UploadRawPhoto;

            //Check Order
            CustomerOrder order = new CustomerOrder();
            order.SerialNo = curReq.OrderId;
            order = order.Fetch() as CustomerOrder;
            if(order == null)
            {
                LogHelper.WriteLog(typeof(TxUploadRawPhoto), "Invalid Request", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Request";
                return Result.Failed;
            }
            PhotographerId = order.PhotographerId;
            //Check Session
            var res = UpdatePhotographerSession(true);
            if (res != Result.Success)
            {
                LogHelper.WriteLog(typeof(TxUploadRawPhoto), "Failed to update session", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Request";
                return res;
            }
            res = ValidateOrderInfo(order);
            if (order == null)
            {
                LogHelper.WriteLog(typeof(TxUploadRawPhoto), "Could not validate Order", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Could not validate Order";
                return Result.Failed;
            }
            if(!order.Paid || order.Status != (int)OrderStatus.OrderConfirmed)
            {
                LogHelper.WriteLog(typeof(TxUploadRawPhoto), "Order is not paid yet", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Order is not paid yet";
                return Result.Failed;
            }

            //Validate Photo Info
            if(curReq.RawPhotos.Count <=0)
            {
                LogHelper.WriteLog(typeof(TxUploadRawPhoto), "No Photos to upload", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "No Photos to upload";
                return Result.Failed;
            }
            var resp = new UploadRawPhotoResponse();
            resp.OrderId = order.SerialNo;
            resp.PhotoPaths = new List<string>();
            foreach (var photo in curReq.RawPhotos)
            {
                if(photo.PhotoName == null || photo.PhotoName.Trim().Length ==0)
                {
                    LogHelper.WriteLog(typeof(TxUploadRawPhoto), "Invalid Photo Name", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Invalid Photo Name";
                    return Result.Failed;
                }

                if(photo.Path == null || photo.Path.Trim().Length == 0)
                {
                    LogHelper.WriteLog(typeof(TxUploadRawPhoto), "Invalid Path", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Invalid Path";
                    return Result.Failed;
                }

                if(photo.Retouched)
                {
                    LogHelper.WriteLog(typeof(TxUploadRawPhoto), "Cannot upload Retouched Photo", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Cannot upload Retouched Photo";
                    return Result.Failed;
                }
                photo.CustomerOrderId = order.SerialNo;
                Data.AddNew(null, photo);
                resp.PhotoPaths.Add(photo.Path);
            }
            response = resp;
            return Result.Success;
        }
        public override Result Prepare()
        {
            //Get PhotoId
            var photoList = Data.GetEntityListByType<Photo>();
            foreach (var photo in photoList)
            {
                (photo.NewEntity as Photo).PhotoId = GetNextId<Photo>();
            }

            return Data.Validate();
        }
        public override Result Update()
        {
            return Data.Update();
        }
    }
}
