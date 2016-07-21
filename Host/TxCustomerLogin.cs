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
    public class TxCustomerLogin:Tx
    {
        public TxCustomerLogin()
        {
            TxnId = 3;
        }
        public override Result Validate()
        {
            CustomerLogin curReq = request as CustomerLogin;
            if (curReq.Email == null || curReq.Email.Length == 0)
            {
                LogHelper.WriteLog(typeof(TxPhotographerLogin), "Invalid Email", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Email";
                return Result.Failed;
            }
            if (curReq.Password == null || curReq.Password.Length == 0)
            {
                LogHelper.WriteLog(typeof(TxPhotographerLogin), "Invalid Password", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Password";
                return Result.Failed;
            }

            //Check if customer exist
            Customer ctm = null;
            using (var context = new HostDBDataContext())
            {
                ctm = context.Customer.Where(c => c.Email == curReq.Email).FirstOrDefault();
            }
            if (ctm == null)
            {
                LogHelper.WriteLog(typeof(TxPhotographerLogin), "Invalid Email and Password", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Email and Password";
                return Result.Failed;
            }
            //Check password if correct
            var valid = PasswordHash.ValidatePassword(curReq.Password, ctm.Password);
            if (!valid)
            {
                LogHelper.WriteLog(typeof(TxPhotographerLogin), "Invalid Password", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Password";
                return Result.Failed;
            }
            CustomerId = ctm.CustomerId;
            //Update Customer
            var newCtm = ctm.Clone() as Customer;
            newCtm.LastLoginTime = DateTime.UtcNow;
            Data.AddNew(ctm, newCtm);

            //Create or Update Session
            var res = UpdateCustomerSession();
            if(res != Result.Success)
            {
                LogHelper.WriteLog(typeof(TxPhotographerLogin), "Failed to Update Session", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.UpdateSessionFailed;
                response.ErrorMsg = "Failed to Update Session";
                return res;
            }
            var resp = new CustomerLoginResponse();
            resp.CustomerId = CustomerId;
            var s = Data.GetEntityListByType<CustomerSession>().First();
            resp.SessionId = (s.NewEntity as CustomerSession).SessionId;
            resp.SessionKey = (s.NewEntity as CustomerSession).SessionKey;
            resp.CustomerName = newCtm.CustomerName;
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
