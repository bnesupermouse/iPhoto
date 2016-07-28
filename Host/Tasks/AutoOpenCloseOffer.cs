using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HostDB;
using Host.Models;

namespace Host.Tasks
{
    public class AutoOpenCloseOffer
    {
        public static void Processing()
        {
            while(true)
            {
                List<UpdOffer> OpenOffers = new List<UpdOffer>();
                List<UpdOffer> CloseOffers = new List<UpdOffer>();
                using (var dc = new HostDBDataContext())
                {
                    var toOpenOffers = from o in dc.Offer
                                       join p in dc.OfferPhotographer on o.OfferId equals p.OfferId
                                       join ph in dc.Photographer on p.PhotographerId equals ph.PhotographerId
                                       where o.StartTime <= DateTime.UtcNow && (o.Status == 1 || o.Status == 3)
                                       select new UpdOffer
                                       {
                                           OldOffer = o,
                                           PhotographerId = ph.PhotographerId,
                                           Action = 2
                                       };
                    OpenOffers.AddRange(toOpenOffers.ToList());
                    foreach(var o in OpenOffers)
                    {
                        o.NewOffer = o.OldOffer.Clone() as Offer;
                        o.NewOffer.Status = 2;
                    }
                    var toCloseOffers = from o in dc.Offer
                                        join p in dc.OfferPhotographer on o.OfferId equals p.OfferId
                                        join ph in dc.Photographer on p.PhotographerId equals ph.PhotographerId
                                        where o.EndTime <= DateTime.UtcNow && o.Status == 2
                                        select new UpdOffer
                                        {
                                            OldOffer = o,
                                            PhotographerId = ph.PhotographerId,
                                            Action = 2
                                        };
                    CloseOffers.AddRange(toCloseOffers.ToList());
                    foreach (var o in CloseOffers)
                    {
                        o.NewOffer = o.OldOffer.Clone() as Offer;
                        o.NewOffer.Status = 3;
                    }
                }
                foreach(var o in OpenOffers)
                {
                    TxUpdOffer txn = new TxUpdOffer();
                    txn.request = o;
                    TxnFunc.ProcessTxn(txn);
                }
                foreach (var o in CloseOffers)
                {
                    TxUpdOffer txn = new TxUpdOffer();
                    txn.request = o;
                    TxnFunc.ProcessTxn(txn);
                }
                Thread.Sleep(200);
            }
        }
    }
}
