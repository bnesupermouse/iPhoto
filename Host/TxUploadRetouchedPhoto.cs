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
                return Result.Failed;
            }
            PhotographerId = order.PhotographerId;
            //Check Session
            var res = UpdatePhotographerSession(true);
            if (res != Result.Success)
            {
                return res;
            }
            res = ValidateOrderInfo(order);
            if (order == null)
            {
                return Result.Failed;
            }
            if(order.Status != (int)OrderStatus.PhotoSelected && order.Status != (int)OrderStatus.RetouchedPhotoConfirming)
            {
                return Result.Failed;
            }

            if (order.Status == (int)OrderStatus.PhotoSelected)
            {
                //Construct new Order
                CustomerOrder newOrder = order.Clone() as CustomerOrder;
                newOrder.Status = curReq.LastPhoto ? (int)OrderStatus.RetouchedPhotoUploaded : (int)OrderStatus.RetouchedPhotoUploading;
                Data.AddNew(order, newOrder);
            }

            //Validate Photo Info
            if(!curReq.LastPhoto && curReq.RetouchedPhotos.Count <=0)
            {
                return Result.Failed;
            }
            var resp = new UploadRawPhotoResponse();
            resp.OrderId = order.SerialNo;
            resp.PhotoPaths = new List<string>();
            foreach (var photo in curReq.RetouchedPhotos)
            {
                if(photo.PhotoName == null || photo.PhotoName.Trim().Length ==0)
                {
                    return Result.Failed;
                }

                if(photo.Path == null || photo.Path.Trim().Length == 0)
                {
                    return Result.Failed;
                }
                if(photo.Confirmed)
                {
                    return Result.Failed;
                }
                if(!photo.Retouched || !photo.Selected)
                {
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
