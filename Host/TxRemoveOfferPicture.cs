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
    public class TxRemoveOfferPicture : Tx
    {
        public TxRemoveOfferPicture()
        {
            TxnId = 1;
        }

        public override Result Validate()
        {
            var curReq = request as RemoveOfferPicture;
            //Check Session
            var res = UpdatePhotographerSession(true);
            if(res != Result.Success)
            {
                return res;
            }

            //Check Offer
            Offer offer = new Offer();
            offer.OfferId = curReq.OfferId;
            offer = offer.Fetch() as Offer;
            if (offer == null)
            {
                return Result.Failed;
            }

            //Check OfferPhotographer
            OfferPhotographer op = new OfferPhotographer();
            op.OfferId = offer.OfferId;
            op.PhotographerId = PhotographerId;
            op = op.Fetch() as OfferPhotographer;
            if (op == null)
            {
                return Result.Failed;
            }

            //Validate Pic Info
            if(curReq.Pictures.Count <=0)
            {
                return Result.Failed;
            }
            var resp = new RemoveOfferPictureResponse();
            resp.OfferId = offer.OfferId;
            foreach (var pic in curReq.Pictures)
            {
                //Check OfferPicture
                OfferPicture opic = new OfferPicture();
                opic.OfferPictureId = pic;
                opic = op.Fetch() as OfferPicture;
                if (opic == null)
                {
                    return Result.Failed;
                }
                Data.AddNew(null, opic);
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
