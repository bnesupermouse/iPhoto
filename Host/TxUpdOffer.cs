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
    public class TxUpdOffer:Tx
    {
        public TxUpdOffer()
        {
            TxnId = 2;
        }
        public int Action{get;set;}
        public Offer OldOffer{get;set;}
        public Offer NewOffer{get;set;}

        public override Result Validate()
        {
            Result res = Result.Success;
            Action = (request as UpdOffer).Action;
            OldOffer = (request as UpdOffer).OldOffer;
            NewOffer = (request as UpdOffer).NewOffer;
            PhotographerId = (request as UpdOffer).PhotographerId;

            //Add
            if (Action == 1 || Action == 2)
            {
                //Check PhotographerId
                Photographer ph = new Photographer();
                ph.PhotographerId = PhotographerId;
                ph = ph.Fetch() as Photographer;
                if (ph == null)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid PhotographerId", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                if(ph.Status != 1)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Photographer is not Verified", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                if (NewOffer == null)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "New Offer cannot be NULL", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                //Check OfferName
                if (NewOffer.OfferName == null || NewOffer.OfferName.Trim().Length == 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid OfferName", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                //Check Description
                if (NewOffer.Description == null || NewOffer.Description.Length > 500)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Offer Name too long", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                //Check Price
                if (NewOffer.Price == null)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Price cannot be NULL", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                else
                {
                    //Check Price complexity
                    if (NewOffer.Price <= 0 || NewOffer.Price > 5000)
                    {
                        LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid Price", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        return Result.Failed;
                    }
                }

                //Check StartTime and EndTime
                if (NewOffer.StartTime != null && NewOffer.EndTime != null)
                {
                    if (NewOffer.StartTime >= NewOffer.EndTime)
                    {
                        LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid StartTime and EndTime", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        return Result.Failed;
                    }
                    if (NewOffer.EndTime <= DateTime.UtcNow.AddDays(1))
                    {
                        LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid EndTime", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        return Result.Failed;
                    }
                }

                //Check PhotoTypeId
                if (NewOffer.PhotoTypeId <= 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid PhotoTypeId", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                else
                {
                    PhotoType phType = new PhotoType();
                    phType.PhotoTypeId = NewOffer.PhotoTypeId;
                    phType = phType.Fetch() as PhotoType;
                    if (phType == null)
                    {
                        LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid PhotoTypeId", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        return Result.Failed;
                    }
                }

                //Check NoServicer
                if (NewOffer.NoServicer <= 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid NoServicer", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }

                //Check MaxPeople
                if (NewOffer.MaxPeople != null)
                {
                    if (NewOffer.MaxPeople <= 0)
                    {
                        LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid MaxPeople", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        return Result.Failed;
                    }
                }

                //Check NoOriginalPhoto
                if (NewOffer.NoRawPhoto <= 0 || NewOffer.NoRawPhoto > 500)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid NoOriginalPhoto", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }

                //Check NoRefinedPhoto
                if (NewOffer.NoRetouchedPhoto <= 0 || NewOffer.NoRetouchedPhoto > 500 || NewOffer.NoRetouchedPhoto > NewOffer.NoRawPhoto)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid NoOriginalPhoto", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                //Check NoMakeup
                if (NewOffer.NoMakeup < 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid NoMakeup", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }

                //Check NoCostume
                if (NewOffer.NoCostume < 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid NoCostume", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                //Check NoPlaces
                if (NewOffer.NoVenue < 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid NoPlaces", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                //Check AdditionalRefinePrice
                if (NewOffer.AdditionalRetouchPrice < 0 || NewOffer.AdditionalRetouchPrice > 5000)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid AdditionalRefinePrice", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }

                //Check DurationHour
                if (NewOffer.DurationHour <= 0)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid DurationHour", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }
                //Check Comment
                if (NewOffer.Comment != null && NewOffer.Comment.Length > 500)
                {
                    LogHelper.WriteLog(typeof(TxUpdOffer), "Comment too long", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    return Result.Failed;
                }

                if (Action == 1)
                {
                    NewOffer.Status = 0;
                    if ((request as UpdOffer).VenueIds != null)
                    {
                        foreach (var venue in (request as UpdOffer).VenueIds)
                        {
                            Venue v = new Venue();
                            v.VenueId = venue;
                            v = v.Fetch() as Venue;
                            if (v == null)
                            {
                                LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid VenueId", Log4NetLevel.Error);
                                response.ErrorNo = (int)Errors.InvalidRequest;
                                return Result.Failed;
                            }
                        }
                    }
                }
                else
                {
                    if ((request as UpdOffer).VenueChanged)
                    {
                        using (var dc = new HostDBDataContext())
                        {
                            var venueList = dc.OfferVenue.Where(ov=>ov.OfferId == NewOffer.OfferId).ToList();
                            foreach (var v in venueList)
                            {
                                Data.AddNew(v, null);
                            }
                        }
                    }
                    //Check OfferPhotographer
                    //OfferPhotographer op = new OfferPhotographer();
                    //op.OfferId = NewOffer.OfferId;
                    //op.PhotographerId = PhotographerId;
                    //op = op.Fetch() as OfferPhotographer;
                    //if (op == null)
                    //{
                    //    LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid PhotographerId and OfferId", Log4NetLevel.Error);
                    //    response.ErrorNo = (int)Errors.InvalidRequest;
                    //    return Result.Failed;
                    //}
                    //Validate Status value
                    if(NewOffer.Status != 0 && NewOffer.Status !=1 && NewOffer.Status !=2 && NewOffer.Status !=3)
                    {
                        LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid Status value", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Status Update";
                        return Result.Failed;
                    }
                    if(OldOffer.Status != 0 && NewOffer.Status == 1)
                    {
                        LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid Status value update", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Status Update";
                        return Result.Failed;
                    }
                    if (OldOffer.Status != NewOffer.Status && OldOffer.Status != 1 && OldOffer.Status != 3 && NewOffer.Status == 2)
                    {
                        LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid Status value update", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Status Update";
                        return Result.Failed;
                    }
                    if (OldOffer.Status == 0 && NewOffer.Status == 1)
                    {
                        if(!ph.Admin)
                        {
                            LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid Status value update", Log4NetLevel.Error);
                            response.ErrorNo = (int)Errors.InvalidRequest;
                            response.ErrorMsg = "Invalid Status Update";
                            return Result.Failed;
                        }
                    }
                    if (OldOffer.Status != 0 && NewOffer.Status == 0)
                    {
                        LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid Status value update", Log4NetLevel.Error);
                        response.ErrorNo = (int)Errors.InvalidRequest;
                        response.ErrorMsg = "Invalid Status Update";
                        return Result.Failed;
                    }
                }
            }
            else
            {
                LogHelper.WriteLog(typeof(TxUpdOffer), "Invalid Action value", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                return Result.Failed;
            }
            return Result.Success;
        }
        public override Result Prepare()
        {
            var resp = new UpdOfferResponse();
            if (Action == 1)
            {
                NewOffer.OfferId = GetNextId<Offer>();
                Data.AddNew(null, NewOffer);
                OfferPhotographer newOfferPh = new OfferPhotographer();
                newOfferPh.OfferId = NewOffer.OfferId;
                newOfferPh.PhotographerId = PhotographerId;
                Data.AddNew(null, newOfferPh);
                if ((request as UpdOffer).VenueIds != null)
                {
                    foreach (var venue in (request as UpdOffer).VenueIds)
                    {
                        OfferVenue ov = new OfferVenue();
                        ov.OfferId = NewOffer.OfferId;
                        ov.VenueId = venue;
                        Data.AddNew(null, ov);
                    }
                }
            }
            else
            {
                Data.AddNew(OldOffer, NewOffer);
                if ((request as UpdOffer).VenueChanged)
                {
                    foreach (var venue in (request as UpdOffer).VenueIds)
                    {
                        OfferVenue ov = new OfferVenue();
                        ov.OfferId = NewOffer.OfferId;
                        ov.VenueId = venue;
                        Data.AddNew(null, ov);
                    }
                }
            }
            resp.OfferId = NewOffer.OfferId;
            resp.PhotographerId = PhotographerId;
            resp.OfferName = NewOffer.OfferName;
            response = resp;
            return Data.Validate();
        }
        public override Result Update()
        {
            return Data.Update();
        }
    }
}
