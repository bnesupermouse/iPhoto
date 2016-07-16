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
    public class TxUploadOfferPicture : Tx
    {
        public TxUploadOfferPicture()
        {
            TxnId = 1;
        }

        public override Result Validate()
        {
            var curReq = request as UploadOfferPicture;
            //Check Session
            
            //Check Offer
            Offer offer = new Offer();
            offer.OfferId = curReq.OfferId;
            offer = offer.Fetch() as Offer;
            if (offer == null)
            {
                return Result.Failed;
            }
            PhotographerId = curReq.PhotographerId;
            //Check OfferPhotographer
            OfferPhotographer op = new OfferPhotographer();
            op.OfferId = offer.OfferId;
            op.PhotographerId = PhotographerId;
            op = op.Fetch() as OfferPhotographer;
            if (op == null)
            {
                return Result.Failed;
            }
            PhotographerId = op.PhotographerId;

            var res = UpdatePhotographerSession(true);
            if (res != Result.Success)
            {
                return res;
            }

            //Validate Pic Info
            if (curReq.Pictures.Count <=0)
            {
                return Result.Failed;
            }
            var resp = new UploadOfferPictureResponse();
            resp.OfferId = offer.OfferId;
            resp.PicturePaths = new List<string>();
            foreach (var pic in curReq.Pictures)
            {
                if (pic.PictureName == null || pic.PictureName.Trim().Length == 0)
                {
                    return Result.Failed;
                }

                if(pic.Path == null || pic.Path.Trim().Length == 0)
                {
                    return Result.Failed;
                }

                if(pic.Description != null && pic.Description.Length > 150)
                {
                    return Result.Failed;
                }
                pic.OfferId = offer.OfferId;
                Data.AddNew(null, pic);
                resp.PicturePaths.Add(pic.Path);
            }
            response = resp;
            return Result.Success;
        }
        public override Result Prepare()
        {
            //Get OfferPictureId
            var picList = Data.GetEntityListByType<OfferPicture>();
            foreach (var pic in picList)
            {
                (pic.NewEntity as OfferPicture).OfferPictureId = GetNextId<OfferPicture>();
            }

            return Data.Validate();
        }
        public override Result Update()
        {
            return Data.Update();
        }
    }
}
