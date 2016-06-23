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
    public class TxUpdPhotoType:Tx
    {
        public TxUpdPhotoType()
        {
            TxnId = 2;
        }
        public int Action{get;set;}
        public PhotoType OldPhotoType { get; set; }
        public PhotoType NewPhotoType { get; set; }
        public PhotographerSession NewSession{get;set;}

        public override Result Validate()
        {
            Result res = Result.Success;
            Action = (request as UpdPhotoType).Action;
            OldPhotoType = (request as UpdPhotoType).OldPhotoType;
            NewPhotoType = (request as UpdPhotoType).NewPhotoType;

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
                if (NewPhotoType == null)
                {
                    LogHelper.WriteLog(typeof(TxUpdPhotographer), "New Photographer cannot be NULL", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                //Check PhotoTypeName
                if (NewPhotoType.PhotoTypeName == null || NewPhotoType.PhotoTypeName.Length == 0)
                {
                    return Result.Failed;
                }
                Data.AddNew(OldPhotoType, NewPhotoType);
            }
            else if(Action == 3)
            {
                Data.AddNew(OldPhotoType, null);
            }
            return Result.Success;
        }
        public override Result Prepare()
        {
            var resp = new UpdPhotoTypeResponse();
            if (Action == 1)
            {
                NewPhotoType.PhotoTypeId = GetNextId<PhotoType>();
                Data.AddNew(null, NewPhotoType);
                resp.PhotoTypeId = NewPhotoType.PhotoTypeId;
            }
            else if(Action == 2 || Action == 3)
            {
                resp.PhotoTypeId = OldPhotoType.PhotoTypeId;
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
