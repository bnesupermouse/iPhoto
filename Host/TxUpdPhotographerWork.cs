using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Host;
using HostDB;
using System.Text.RegularExpressions;
using HostMessage.Responses;
using Host.Common;

namespace Host
{
    public class TxUpdPhotographerWork:Tx
    {
        public TxUpdPhotographerWork()
        {
            TxnId = 2;
        }
        public int Action{get;set;}
        public PhotographerWork OldPhotographerWork { get; set; }
        public PhotographerWork NewPhotographerWork { get; set; }
        public PhotographerSession NewSession{get;set;}

        public override Result Validate()
        {
            Result res = Result.Success;
            Action = (request as UpdPhotographerWork).Action;
            OldPhotographerWork = (request as UpdPhotographerWork).OldPhotographerWork;
            NewPhotographerWork = (request as UpdPhotographerWork).NewPhotographerWork;

            //Update Session time
            res = UpdatePhotographerSession();
            if (res != Result.Success)
            {
                LogHelper.WriteLog(typeof(TxUpdPhotographer), "Failed to update session", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.UpdateSessionFailed;
                return Result.Failed;
            }

            //Add
            if (Action == 1 || Action == 2)
            {
                if(Action ==1)
                {
                    NewPhotographerWork.PhotographerId = PhotographerId;
                }
                else
                {
                    if(OldPhotographerWork.PhotographerId != NewPhotographerWork.PhotographerId)
                    {
                        return Result.Failed;
                    }
                }
                if (NewPhotographerWork == null)
                {
                    LogHelper.WriteLog(typeof(TxUpdPhotographer), "New Photographer cannot be NULL", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                //Check PhotographerId
                Photographer ph = new Photographer();
                ph.PhotographerId = PhotographerId;
                ph = ph.Fetch() as Photographer;
                if (ph == null)
                {
                    return Result.Failed;
                }
                //Check Name
                if (NewPhotographerWork.Name == null || NewPhotographerWork.Name.Trim().Length == 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdPhotographer), "Invalid Name", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }

                //Check PhotoType
                PhotoType pt = new PhotoType();
                pt.PhotoTypeId = NewPhotographerWork.PhotoTypeId;
                pt = pt.Fetch() as PhotoType;
                if (pt == null)
                {
                    return Result.Failed;
                }
            }
            else if(Action == 3)
            {
                using (var dc = new HostDBDataContext())
                {
                    var picList = dc.PhotographerWorkPicture.Where(p => p.PhotographerWorkId == OldPhotographerWork.PhotographerWorkId).ToList();
                    foreach (var pic in picList)
                    {
                        Data.AddNew(pic, null);
                    }
                    Data.AddNew(OldPhotographerWork, null);
                }
            }
            return Result.Success;
        }
        public override Result Prepare()
        {
            var resp = new UpdPhotographerWorkResponse();
            if (Action == 1)
            {
                NewPhotographerWork.PhotographerWorkId = GetNextId<PhotographerWork>();
                Data.AddNew(null, NewPhotographerWork);
                resp.PhotographerId = NewPhotographerWork.PhotographerId;
                resp.PhotographerWorkId = NewPhotographerWork.PhotographerWorkId;
            }
            else if(Action == 2)
            {
                Data.AddNew(OldPhotographerWork, NewPhotographerWork);
                resp.PhotographerId = PhotographerId;
                resp.PhotographerWorkId = OldPhotographerWork.PhotographerWorkId;
            }
            response = resp;
            return Data.Validate();
        }
        public override Result Update()
        {
            return Data.Update();
        }
    }
}
