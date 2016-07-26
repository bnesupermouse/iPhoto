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
    public class TxUploadRetouchedPhoto : Tx
    {
        public TxUploadRetouchedPhoto()
        {
            TxnId = 1;
        }

        public override Result Validate()
        {
            var curReq = request as UploadRetouchedPhoto;
            //Check Order
            CustomerOrder order = new CustomerOrder();
            order.SerialNo = curReq.OrderId;
            order = order.Fetch() as CustomerOrder;
            if(order == null)
            {
                LogHelper.WriteLog(typeof(TxUploadRetouchedPhoto), "Invalid Request", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Request";
                return Result.Failed;
            }
            PhotographerId = order.PhotographerId;
            //Check Session
            var res = UpdatePhotographerSession(true);
            if (res != Result.Success)
            {
                LogHelper.WriteLog(typeof(TxUploadRetouchedPhoto), "Failed to Update Session", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Failed to Update Session";
                return res;
            }
            res = ValidateOrderInfo(order);
            if (order == null)
            {
                LogHelper.WriteLog(typeof(TxUploadRetouchedPhoto), "Failed to validate Order", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Failed to Validate Order";
                return Result.Failed;
            }
            if(order.Status != (int)OrderStatus.PhotoSelected)
            {
                LogHelper.WriteLog(typeof(TxUploadRetouchedPhoto), "Raw Photos have not been selected", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Raw Photos have not been selected";
                return Result.Failed;
            }

            //Validate Photo Info
            if(!curReq.LastPhoto && curReq.RetouchedPhotos.Count <=0)
            {
                LogHelper.WriteLog(typeof(TxUploadRetouchedPhoto), "No Photo to upload", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "No Photo to upload";
                return Result.Failed;
            }
            var resp = new UploadRetouchedPhotoResponse();
            resp.OrderId = order.SerialNo;
            resp.PhotoPaths = new List<string>();
            foreach (var photo in curReq.RetouchedPhotos)
            {
                if(photo.PhotoName == null || photo.PhotoName.Trim().Length ==0)
                {
                    LogHelper.WriteLog(typeof(TxUploadRetouchedPhoto), "Invalid Photo Name", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Invalid Photo Name";
                    return Result.Failed;
                }

                if(photo.Path == null || photo.Path.Trim().Length == 0)
                {
                    LogHelper.WriteLog(typeof(TxUploadRetouchedPhoto), "Invalid Path", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Invalid Path";
                    return Result.Failed;
                }
                if(photo.Confirmed)
                {
                    LogHelper.WriteLog(typeof(TxUploadRetouchedPhoto), "Photo cannot be confirmed", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Photo cannot be confirmed";
                    return Result.Failed;
                }
                if(!photo.Retouched || !photo.Selected)
                {
                    LogHelper.WriteLog(typeof(TxUploadRetouchedPhoto), "Photo is not selected and retouched", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Photo is not selected and retouched";
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
