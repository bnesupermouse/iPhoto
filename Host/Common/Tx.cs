using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HostDB;

namespace Host
{
    public class Tx
    {
        public Request request = null;
        public Response response = null;
        public DataSet Data = new DataSet();
        public int TxnId { get; set; }
        public long SerialNo { get; set; }
        public long CustomerId { get; set; }
        public long PhotographerId { get; set; }
        public DateTime TxnTime { get; set; }
        public int ErrorNo { get; set; }
        public Tx()
        {
            CustomerId = 0;
            ErrorNo = 0;
            request = new Request();
            response = new Response();
        }

        public virtual Result Validate()
        {
            return Result.Success;
        }
        public virtual Result Prepare()
        {
            return Result.Success;
        }
        public virtual Result Update()
        {
            return Result.Success;
        }

        public Result UpdateCustomerSession(bool mustExisting  = false)
        {
            try
            {
                CustomerSession OldSession = new CustomerSession();
                if (request.SessionId > 0)
                {
                    OldSession.SessionId = request.SessionId;
                    OldSession = OldSession.Fetch() as CustomerSession;
                }
                else
                {
                    using (var dc = new HostDBDataContext())
                    {
                        OldSession = dc.CustomerSession.Where(s => s.CustomerId == CustomerId).FirstOrDefault();
                    }
                }
                if (OldSession != null)
                {
                    if (OldSession.Status != 0)
                    {
                        return Result.Failed;
                    }
                    CustomerSession NewSession = OldSession.Clone() as CustomerSession;
                    NewSession.LastUseTime = DateTime.UtcNow;
                    NewSession.SessionKey = Guid.NewGuid().ToString();
                    Data.AddNew(OldSession, NewSession);
                }
                else
                {
                    if (!mustExisting)
                    {
                        if (CustomerId == 0)
                        {
                            return Result.Failed;
                        }
                        CustomerSession NewSession = new CustomerSession();
                        NewSession.CustomerId = CustomerId;
                        NewSession.SessionId = GetNextId<CustomerSession>();
                        NewSession.SessionKey = Guid.NewGuid().ToString();
                        NewSession.LastUseTime = DateTime.UtcNow;
                        NewSession.Status = 0;
                        Data.AddNew(null, NewSession);
                    }
                    else
                    {
                        return Result.Failed;
                    }
                }
                return Result.Success;
            }
            catch (Exception ex)
            {
                return Result.Failed;
            }
        }


        public Result UpdatePhotographerSession(bool mustExisting = false)
        {
            try
            {
                PhotographerSession OldSession = new PhotographerSession();
                if (request.SessionId > 0)
                {
                    OldSession.SessionId = request.SessionId;
                    OldSession = OldSession.Fetch() as PhotographerSession;
                }
                else
                {
                    using (var dc = new HostDBDataContext())
                    {
                        OldSession = dc.PhotographerSession.Where(s => s.PhotographerId == PhotographerId).FirstOrDefault();
                    }
                }
                if (OldSession != null)
                {
                    if (OldSession.Status != 0)
                    {
                        return Result.Failed;
                    }
                    PhotographerId = OldSession.PhotographerId;
                    PhotographerSession NewSession = OldSession.Clone() as PhotographerSession;
                    NewSession.LastUseTime = DateTime.UtcNow;
                    NewSession.SessionKey = Guid.NewGuid().ToString();
                    Data.AddNew(OldSession, NewSession);
                }
                else
                {
                    if (!mustExisting)
                    {
                        if (PhotographerId == 0)
                        {
                            return Result.Failed;
                        }
                        PhotographerSession NewSession = new PhotographerSession();
                        NewSession.PhotographerId = PhotographerId;
                        NewSession.SessionId = GetNextId<PhotographerSession>();
                        NewSession.SessionKey = Guid.NewGuid().ToString();
                        NewSession.LastUseTime = DateTime.UtcNow;
                        NewSession.Status = 0;
                        Data.AddNew(null, NewSession);
                    }
                    else
                    {
                        return Result.Failed;
                    }
                }
                return Result.Success;
            }
            catch (Exception ex)
            {
                return Result.Failed;
            }
        }

        public Result ValidateOrderInfo(CustomerOrder order)
        {
            //Check Customer
            Customer customer = new Customer();
            customer.CustomerId = order.CustomerId;
            customer = customer.Fetch() as Customer;
            if (customer == null)
            {
                return Result.Failed;
            }
            if (customer.Status != 0)
            {
                return Result.Failed;
            }

            //Check Offer
            Offer offer = new Offer();
            offer.OfferId = order.OfferId;
            offer = offer.Fetch() as Offer;
            if (offer == null)
            {
                return Result.Failed;
            }
            if (offer.Status != 0 && offer.Status !=1 && offer.Status !=2)
            {
                return Result.Failed;
            }

            //Check Offer Time Range
            //TODO

            //Check Photographer
            Photographer photographer = new Photographer();
            photographer.PhotographerId = order.PhotographerId;
            photographer = photographer.Fetch() as Photographer;
            if (photographer == null)
            {
                return Result.Failed;
            }

            if (photographer.Status != 0 && photographer.Status != 1 && photographer.Status != 2)
            {
                return Result.Failed;
            }

            //Check OfferPhotographer
            OfferPhotographer offerph = new OfferPhotographer();
            offerph.OfferId = offer.OfferId;
            offerph.PhotographerId = photographer.PhotographerId;
            offerph = offerph.Fetch() as OfferPhotographer;
            if (offerph == null)
            {
                return Result.Failed;
            }
            return Result.Success;
        }

        public long GetNextId<T>() where T :class
        {
            string name =typeof(T).Name;
            string nextName = "Next" + typeof(T).Name + "Id";
            using (var dc = new HostDBDataContext())
            {
                var global = dc.GlobalValue.Where(g=>g.GlbName == nextName).FirstOrDefault();
                if(global == null)
                {
                    global = new GlobalValue();
                    global.GlbName = "Next" + name + "Id";
                    global.GlbValue = 2;
                    Data.AddNew(null, global);
                    return 1;
                }
                else
                {
                    var newGlobal = global.Clone() as GlobalValue;
                    newGlobal.GlbValue += 1;
                    Data.AddNew(global, newGlobal);
                    return global.GlbValue;
                }
            }
        }
    }
}
