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
    public class TxUpdPhotographer:Tx
    {
        public TxUpdPhotographer()
        {
            TxnId = 2;
        }
        public int Action{get;set;}
        public Photographer OldPhotographer { get; set; }
        public Photographer NewPhotographer { get; set; }
        public PhotographerSession NewSession{get;set;}

        public override Result Validate()
        {
            Result res = Result.Success;
            Action = (request as UpdPhotographer).Action;
            OldPhotographer = (request as UpdPhotographer).OldPhotographer;
            NewPhotographer = (request as UpdPhotographer).NewPhotographer;

            //Add
            if (Action == 1 || Action == 2)
            {
                if (NewPhotographer == null)
                {
                    LogHelper.WriteLog(typeof(TxUpdPhotographer), "New Photographer cannot be NULL", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Invalid Request";
                    return Result.Failed;
                }
                //Check Email
                if (NewPhotographer.Email == null || NewPhotographer.Email.Trim().Length == 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdPhotographer), "Invalid Email", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Invalid Email";
                    return Result.Failed;
                }
                else
                {
                    if (Action == 2 && OldPhotographer.Email != OldPhotographer.Email)
                    {
                        LogHelper.WriteLog(typeof(TxUpdPhotographer), "Emails are not same in Request", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Emails in Request";
                        return Result.Failed;
                    }
                    else
                    {
                        res = Utility.ValidateEmail(NewPhotographer.Email);
                        if (res != Result.Success)
                        {
                            LogHelper.WriteLog(typeof(TxUpdPhotographer), "Email Validation Failed", Log4NetLevel.Error);
                            response.ErrorNo = (int)Errors.InvalidRequest;
                            response.ErrorMsg = "Email Validation Failed";
                            return res;
                        }
                    }
                }
                if(NewPhotographer.PayRate >100)
                {
                    return Result.Failed;
                }
                if(NewPhotographer.PayRate ==0)
                {
                    NewPhotographer.PayRate = 70;
                }
                if (Action == 1)
                {
                    var dc = new HostDBDataContext();
                    bool exist = dc.Photographer.Where(c => c.Email == NewPhotographer.Email).Count() > 0;
                    if (exist)
                    {
                        LogHelper.WriteLog(typeof(TxUpdPhotographer), "Email already registered", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.EmailAlreadyRegistered;
                        response.ErrorMsg = "Email already registered";
                        return Result.Failed;
                    }
                }
                //Check PhotographerName
                if (NewPhotographer.PhotographerName == null || NewPhotographer.PhotographerName.Trim().Length == 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdPhotographer), "Photographer Name cannot be NULL", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Photographer Name cannot be Empty";
                    return Result.Failed;
                }

                //Check Password
                if (NewPhotographer.Password == null || NewPhotographer.Password.Trim().Length == 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdPhotographer), "Password cannot be NULL", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Password cannot be Empty";
                    return Result.Failed;
                }
                else
                {
                    //Check Password complexity

                }
                //Hash Password
                string pwdHash = PasswordHash.HashPassword(NewPhotographer.Password);
                NewPhotographer.Password = pwdHash;

                //Check Gender when need
                if (NewPhotographer.Gender != null)
                {
                    if (NewPhotographer.Gender != 0 && NewPhotographer.Gender != 1)
                    {
                        LogHelper.WriteLog(typeof(TxUpdPhotographer), "Invalid Gender value", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Gender value";
                        return Result.Failed;
                    }
                }

                //Check Age when need
                if (NewPhotographer.Age != null)
                {
                    //Validate Age
                    if (NewPhotographer.Age <= 0 || NewPhotographer.Age >= 100)
                    {
                        LogHelper.WriteLog(typeof(TxUpdPhotographer), "Invalid Age value", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Age value";
                        return Result.Failed;
                    }
                }

                //Check Phone when need
                if (NewPhotographer.Phone != null && NewPhotographer.Phone.Length > 0)
                {
                    //Validate Phone
                }

                //Check ExperienceYear when need
                if (NewPhotographer.ExperienceYear != null)
                {
                    //Validate ExperienceYear
                    if (NewPhotographer.ExperienceYear <= 0 || NewPhotographer.ExperienceYear >= 100)
                    {
                        LogHelper.WriteLog(typeof(TxUpdPhotographer), "Invalid ExperienceYear value", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid ExperienceYear value";
                        return Result.Failed;
                    }
                }

                //Check Introduction when need
                if (NewPhotographer.Introduction != null && NewPhotographer.Introduction.Length > 0)
                {
                    //Validate Introduction
                }

                //Check Rank when need
                if (NewPhotographer.Rank != null)
                {
                    //Validate Rank
                    if (NewPhotographer.Rank < 0 || NewPhotographer.Rank > 5)
                    {
                        LogHelper.WriteLog(typeof(TxUpdPhotographer), "Invalid Rank value", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Rank value";
                        return Result.Failed;
                    }
                }

                //Check LikeVote when need
                if (NewPhotographer.LikeVote != null)
                {
                    //Validate LikeVote
                    if (NewPhotographer.LikeVote < 0)
                    {
                        LogHelper.WriteLog(typeof(TxUpdPhotographer), "Invalid LikeVote value", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid LikeVote value";
                        return Result.Failed;
                    }
                }

                //Check DislikeVote when need
                if (NewPhotographer.DislikeVote != null)
                {
                    //Validate DislikeVote
                    if (NewPhotographer.DislikeVote < 0)
                    {
                        LogHelper.WriteLog(typeof(TxUpdPhotographer), "Invalid DislikeVote value", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid DislikeVote value";
                        return Result.Failed;
                    }
                }

                //Check HeadPhoto when need
                if (NewPhotographer.HeadPhoto != null && NewPhotographer.HeadPhoto.Length > 0)
                {
                    //Validate HeadPhoto
                }

                if (Action == 1)
                {
                    NewPhotographer.OpenDate = DateTime.UtcNow;
                    NewPhotographer.LastLoginTime = NewPhotographer.OpenDate;
                    NewPhotographer.Status = 0;

                    NewSession = new PhotographerSession();
                    NewSession.LastUseTime = NewPhotographer.LastLoginTime;
                    NewSession.Status = 0;
                }
                else
                {
                    //Validate Status value
                    if (NewPhotographer.Status != 0 && NewPhotographer.Status != 1)
                    {
                        LogHelper.WriteLog(typeof(TxUpdPhotographer), "Invalid Status value", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Status value";
                        return Result.Failed;
                    }

                    //Update Session time
                    res = UpdatePhotographerSession();
                    if(res != Result.Success)
                    {
                        LogHelper.WriteLog(typeof(TxUpdPhotographer), "Failed to update session", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.UpdateSessionFailed;
                        response.ErrorMsg = "Failed to update session";
                        return Result.Failed;
                    }

                }
            }
            else
            {
                LogHelper.WriteLog(typeof(TxUpdPhotographer), "Invalid Action value", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Request";
                return Result.Failed;
            }
            return Result.Success;
        }
        public override Result Prepare()
        {
            var resp = new UpdPhotographerResponse();
            if (Action == 1)
            {
                NewPhotographer.PhotographerId = GetNextId<Photographer>();
                NewSession.PhotographerId = NewPhotographer.PhotographerId;
                NewSession.SessionId = GetNextId<PhotographerSession>();
                NewSession.SessionKey = Guid.NewGuid().ToString();
                Data.AddNew(null, NewPhotographer);
                Data.AddNew(null, NewSession);

                //Add PhotographerAccount
                PhotographerAccount pa = new PhotographerAccount();
                pa.PhotographerId = NewPhotographer.PhotographerId;
                pa.Balance = 0;
                pa.PendingBalance = 0;
                pa.TotalBalance = 0;
                Data.AddNew(null, pa);

                resp.PhotographerId = NewPhotographer.PhotographerId;
                resp.SessionId = NewSession.SessionId;
                resp.Email = NewPhotographer.Email;
                resp.SessionKey = NewSession.SessionKey;
                resp.PhotographerName = NewPhotographer.PhotographerName;
            }
            else
            {
                Data.AddNew(OldPhotographer, NewPhotographer);
                resp.PhotographerId = PhotographerId;
                var s = Data.GetEntityListByType<PhotographerSession>().First();
                resp.SessionId = (s.NewEntity as PhotographerSession).SessionId;
                resp.Email = OldPhotographer.Email;
                resp.SessionKey = (s.NewEntity as PhotographerSession).SessionKey;
                resp.PhotographerName = NewPhotographer.PhotographerName;
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
