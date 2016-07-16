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
            var curReq = request as SelectPhoto;
            //Check Order
            CustomerOrder order = new CustomerOrder();
            order.SerialNo = curReq.OrderId;
            order = order.Fetch() as CustomerOrder;
            if (order == null)
            {
                return Result.Failed;
            }
            var res = ValidateOrderInfo(order);
            if (order == null)
            {
                return Result.Failed;
            }
            CustomerId = order.CustomerId;
            //Check Session
             res = UpdateCustomerSession(true);
            if(res != Result.Success)
            {
                return res;
            }

            
            if(order.Status != (int)OrderStatus.RawPhotoUploaded)
            {
                return Result.Failed;
            }
            //Validate Photo Info
            var resp = new SelectRawPhotoResponse();
            resp.OrderId = order.SerialNo;
            resp.PhotoIds = new List<long>();
            foreach (var photo in curReq.SelectedPhotoIds)
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

            foreach (var photo in curReq.DeselectedPhotoIds)
            {
                Photo ph = new Photo();
                ph.PhotoId = photo;
                ph = ph.Fetch() as Photo;
                if (ph == null)
                {
                    return Result.Failed;
                }
                if (!ph.Selected)
                {
                    return Result.Failed;
                }
                var newPh = ph.Clone() as Photo;
                newPh.Selected = false;
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
