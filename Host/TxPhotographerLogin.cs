using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Host;
using HostDB;
using System.Text.RegularExpressions;
using HostMessage.Responses;

namespace Host
{
    public class TxPhotographerLogin : Tx
    {
        public TxPhotographerLogin()
        {
            TxnId = 3;
        }
        public override Result Validate()
        {
            PhotographerLogin curReq = request as PhotographerLogin;
            if (curReq.Email == null || curReq.Email.Length == 0)
            {
                return Result.Failed;
            }
            if (curReq.Password == null || curReq.Password.Length == 0)
            {
                return Result.Failed;
            }

            //Check if Photographer exist
            Photographer photographer = null;
            using (var context = new HostDBDataContext())
            {
                photographer = context.Photographers.Where(c => c.Email == curReq.Email).FirstOrDefault();
            }
            if (photographer == null)
            {
                return Result.Failed;
            }
            //Check password if correct
            var valid = PasswordHash.ValidatePassword(curReq.Password, photographer.Password);
            if (!valid)
            {
                return Result.Failed;
            }
            PhotographerId = photographer.PhotographerId;
            //Update Photographer
            var newPh = photographer.Clone() as Photographer;
            newPh.LastLoginTime = DateTime.UtcNow;
            Data.AddNew(photographer, newPh);

            //Create or Update Session
            var res = UpdatePhotographerSession();
            if(res != Result.Success)
            {
                return res;
            }
            var resp = new PhotographerLoginResponse();
            resp.PhotographerId = PhotographerId;
            var s = Data.GetEntityListByType<PhotographerSession>().First();
            resp.SessionId = (s.NewEntity as PhotographerSession).SessionId;
            resp.SessionKey = (s.NewEntity as PhotographerSession).SessionKey;
            resp.PhotographerName = newPh.PhotographerName;
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
