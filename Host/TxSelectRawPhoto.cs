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
    public class TxSelectRawPhoto : Tx
    {
        public TxSelectRawPhoto()
        {
            TxnId = 1;
        }

        public override Result Validate()
        {
            var curReq = request as SelectRawPhoto;
            //Check Session
            var res = UpdateCustomerSession(true);
            if(res != Result.Success)
            {
                return res;
            }

            //Check Order
            CustomerOrder order = new CustomerOrder();
            order.SerialNo = curReq.OrderId;
            order = order.Fetch() as CustomerOrder;
            if(order == null)
            {
                return Result.Failed;
            }
            res = ValidateOrderInfo(order);
            if (order == null)
            {
                return Result.Failed;
            }
            if(order.Status != (int)OrderStatus.RawPhotoUploaded && order.Status != (int)OrderStatus.PhotoSelecting)
            {
                return Result.Failed;
            }
            if (order.Status == (int)OrderStatus.RawPhotoUploaded)
            {
                //Construct new Order
                CustomerOrder newOrder = order.Clone() as CustomerOrder;
                newOrder.Status = curReq.LastPhoto ? (int)OrderStatus.PhotoSelected : (int)OrderStatus.PhotoSelecting;
                Data.AddNew(order, newOrder);
            }

            //Validate Photo Info
            if(!curReq.LastPhoto && curReq.PhotoIds.Count <=0)
            {
                return Result.Failed;
            }
            var resp = new SelectRawPhotoResponse();
            resp.OrderId = order.SerialNo;
            resp.PhotoIds = new List<long>();
            foreach (var photo in curReq.PhotoIds)
            {
                Photo ph = new Photo();
                ph.PhotoId = photo;
                ph = ph.Fetch() as Photo;
                if(ph == null)
                {
                    return Result.Failed;
                }
                if(ph.Selected || ph.Retouched || ph.Confirmed)
                {
                    return Result.Failed;
                }
                var newPh = ph.Clone() as Photo;
                newPh.Selected = true;
                Data.AddNew(ph, newPh);

                resp.PhotoIds.Add(photo);
            }
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
