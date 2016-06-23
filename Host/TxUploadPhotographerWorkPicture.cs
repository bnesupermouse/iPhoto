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
    public class TxUploadPhotographerWorkPicture : Tx
    {
        public TxUploadPhotographerWorkPicture()
        {
            TxnId = 1;
        }

        public override Result Validate()
        {
            var curReq = request as UploadPhotographerWorkPicture;
            //Check Session
            var res = UpdatePhotographerSession(true);
            if(res != Result.Success)
            {
                return res;
            }

            //Check PhotographerWork
            PhotographerWork pw = new PhotographerWork();
            pw.PhotographerWorkId = curReq.PhotographerWorkId;
            pw = pw.Fetch() as PhotographerWork;
            if (pw == null)
            {
                return Result.Failed;
            }

            //Validate Pic Info
            if(curReq.Pictures.Count <=0)
            {
                return Result.Failed;
            }
            var resp = new UploadPhotographerWorkPictureResponse();
            resp.PhotographerId = PhotographerId;
            resp.PhotographerWorkId = curReq.PhotographerWorkId;
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
                pic.PhotographerWorkId = curReq.PhotographerWorkId;
                Data.AddNew(null, pic);
                resp.PicturePaths.Add(pic.Path);
            }
            response = resp;
            return Result.Success;
        }
        public override Result Prepare()
        {
            //Get PhotographerWorkPictureId
            var picList = Data.GetEntityListByType<PhotographerWorkPicture>();
            foreach (var pic in picList)
            {
                (pic.NewEntity as PhotographerWorkPicture).PhotographerPictureId = GetNextId<PhotographerWorkPicture>();
            }

            return Data.Validate();
        }
        public override Result Update()
        {
            return Data.Update();
        }
    }
}
