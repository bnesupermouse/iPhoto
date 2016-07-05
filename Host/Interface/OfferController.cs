using Host.Models;
using HostDB;
using HostMessage.Responses;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Host
{
    public class OfferController : ApiController
    {
        [HttpPost]
        public Response PlaceOrder(PlaceOrder placeOrder)
        {
            TxPlaceOrder txn = new TxPlaceOrder();
            txn.request = placeOrder;
            var res = TxnFunc.ProcessTxn(txn);
            return txn.response;
        }
        [HttpGet]
       
        public OfferInfo GetOfferDetails(int id)
        {
            using (var dc = new HostDBDataContext())
            {
                var offer = from o in dc.Offers
                join p in dc.OfferPhotographers on o.OfferId equals p.OfferId
                join ph in dc.Photographers on p.PhotographerId equals ph.PhotographerId
                where o.OfferId == id
                select new OfferInfo{ OfferId = o.OfferId, OfferName = o.OfferName, Description = o.Description, PhotographerId = p.PhotographerId
                , PhotographerName=ph.PhotographerName, Price = o.Price, SortOrder = o.SortOrder};
                var res = offer.ToList();
                foreach(var of in res)
                {
                    of.OfferPics = dc.OfferPictures.Where(o => o.OfferId == of.OfferId).Select(o=>o.Path).ToList();
                }
                return res.FirstOrDefault();

            }
        }
    }
}
