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
    public class TxRemovePhotographerWorkPicture : Tx
    {
        public TxRemovePhotographerWorkPicture()
        {
            TxnId = 1;
        }

        public override Result Validate()
        {
            var curReq = request as RemovePhotographerWorkPicture;
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
            var resp = new RemovePhotographerWorkPictureResponse();
            resp.PhotographerWorkId = pw.PhotographerWorkId;
            foreach (var pic in curReq.Pictures)
            {
                //Check PhotographerWorkPicture
                PhotographerWorkPicture op = new PhotographerWorkPicture();
                op.PhotographerPictureId = pic;
                op = op.Fetch() as PhotographerWorkPicture;
                if (op == null)
                {
                    return Result.Failed;
                }
                Data.AddNew(null, op);
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
