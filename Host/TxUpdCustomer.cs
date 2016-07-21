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
    public class TxUpdCustomer:Tx
    {
        public TxUpdCustomer()
        {
            TxnId = 2;
        }
        public int Action{get;set;}
        public Customer OldCustomer{get;set;}
        public Customer NewCustomer{get;set;}
        public CustomerSession NewSession{get;set;}

        public override Result Validate()
        {
            Result res = Result.Success;
            Action = (request as UpdCustomer).Action;
            OldCustomer = (request as UpdCustomer).OldCustomer;
            NewCustomer = (request as UpdCustomer).NewCustomer;

            //Add
            if (Action == 1 || Action == 2)
            {
                if (NewCustomer == null)
                {
                    LogHelper.WriteLog(typeof(TxUpdCustomer), "New Customer cannot be NULL", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Invalid Request";
                    return Result.Failed;
                }
                //Check Email
                if (NewCustomer.Email == null || NewCustomer.Email.Trim().Length == 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdCustomer), "Invalid Email", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Invalid Email";
                    return Result.Failed;
                }
                else
                {
                    if (Action == 2 && OldCustomer.Email != NewCustomer.Email)
                    {
                        LogHelper.WriteLog(typeof(TxUpdCustomer), "Emails are not same in Request", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Email in Request";
                        return Result.Failed;
                    }
                    else
                    {
                        res = Utility.ValidateEmail(NewCustomer.Email);
                        if (res != Result.Success)
                        {
                            LogHelper.WriteLog(typeof(TxUpdCustomer), "Email Validation Failed", Log4NetLevel.Error);
                            response.ErrorMsg = "Email Validation Failed";
                            response.ErrorNo = (int)Errors.InvalidRequest;
                            return res;
                        }
                    }
                }
                if (Action == 1)
                {
                    var dc = new HostDBDataContext();
                    bool exist = dc.Customer.Where(c => c.Email == NewCustomer.Email).Count() > 0;
                    if (exist)
                    {
                        LogHelper.WriteLog(typeof(TxUpdCustomer), "Email already registered", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.EmailAlreadyRegistered;
                        response.ErrorMsg = "Email Already Used";
                        return Result.Failed;
                    }
                }
                //Check CustomerName
                if (NewCustomer.CustomerName == null || NewCustomer.CustomerName.Trim().Length == 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdCustomer), "Customer Name cannot be NULL", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Customer Name cannot be Empty";
                    return Result.Failed;
                }

                //Check Password
                if (NewCustomer.Password == null || NewCustomer.Password.Trim().Length == 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdCustomer), "Password cannot be NULL", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Password cannot be Empty";
                    return Result.Failed;
                }
                else
                {
                    //Check Password complexity

                }
                //Hash Password
                string pwdHash = PasswordHash.HashPassword(NewCustomer.Password);
                NewCustomer.Password = pwdHash;

                //Check Gender when need
                if (NewCustomer.Gender != null)
                {
                    if(NewCustomer.Gender !=0 && NewCustomer.Gender != 1)
                    {
                        LogHelper.WriteLog(typeof(TxUpdCustomer), "Invalid Gender value", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Gender value";
                        return Result.Failed;
                    }
                }

                //Check Age when need
                if (NewCustomer.Age != null)
                {
                    //Validate Age
                    if(NewCustomer.Age <=0 || NewCustomer.Age >=100)
                    {
                        LogHelper.WriteLog(typeof(TxUpdCustomer), "Invalid Age value", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Age value";
                        return Result.Failed;
                    }
                }

                //Check Address when need
                if (NewCustomer.Address != null && NewCustomer.Address.Length > 0)
                {
                    //Validate Address
                }

                //Check Phone when need
                if (NewCustomer.Phone != null && NewCustomer.Phone.Length > 0)
                {
                    //Validate Phone
                }

                if (Action == 1)
                {
                    NewCustomer.OpenDate = DateTime.UtcNow;
                    NewCustomer.LastLoginTime = NewCustomer.OpenDate;
                    NewCustomer.Status = 0;

                    NewSession = new CustomerSession();
                    NewSession.SessionKey = Guid.NewGuid().ToString();
                    NewSession.LastUseTime = NewCustomer.LastLoginTime;
                    NewSession.Status = 0;
                }
                else
                {
                    //Validate Status value
                    if(NewCustomer.Status != 0 && NewCustomer.Status !=1)
                    {
                        LogHelper.WriteLog(typeof(TxUpdCustomer), "Invalid Status value", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Status value";
                        return Result.Failed;
                    }

                    //Update Session time
                    res = UpdateCustomerSession();
                    if(res != Result.Success)
                    {
                        LogHelper.WriteLog(typeof(TxUpdCustomer), "Failed to update session", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.UpdateSessionFailed;
                        response.ErrorMsg = "Failed to update session";
                        return Result.Failed;
                    }

                }
            }
            else
            {
                LogHelper.WriteLog(typeof(TxUpdCustomer), "Invalid Action value", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Request";
                return Result.Failed;
            }
            return Result.Success;
        }
        public override Result Prepare()
        {
            var resp = new UpdCustomerResponse();
            if (Action == 1)
            {
                NewCustomer.CustomerId = GetNextId<Customer>();
                NewSession.CustomerId = NewCustomer.CustomerId;
                NewSession.SessionId = GetNextId<CustomerSession>();
                NewSession.SessionKey = Guid.NewGuid().ToString();
                Data.AddNew(null, NewCustomer);
                Data.AddNew(null, NewSession);
                resp.CustomerId = NewCustomer.CustomerId;
                resp.CustomerName = NewCustomer.CustomerName;
                resp.SessionId = NewSession.SessionId;
                resp.Email = NewCustomer.Email;
                resp.SessionKey = NewSession.SessionKey;
            }
            else
            {
                Data.AddNew(OldCustomer, NewCustomer);
                resp.CustomerId = CustomerId;
                var s = Data.GetEntityListByType<CustomerSession>().First();
                resp.SessionId = (s.NewEntity as CustomerSession).SessionId;
                resp.CustomerId = OldCustomer.CustomerId;
                resp.CustomerName = NewCustomer.CustomerName;
                resp.Email = OldCustomer.Email;
                resp.SessionKey = (s.NewEntity as CustomerSession).SessionKey;
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
