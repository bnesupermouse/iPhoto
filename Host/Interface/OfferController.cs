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
        [HttpPost]
        public Response PayOrder(PayOrder payOrder)
        {
            TxPayOrder txn = new TxPayOrder();
            txn.request = payOrder;
            var res = TxnFunc.ProcessTxn(txn);
            return txn.response;
        }
        [HttpGet]
       
        public OfferInfo GetOfferDetails(int id)
        {
            using (var dc = new HostDBDataContext())
            {
                var offer = from o in dc.Offer
                join p in dc.OfferPhotographer on o.OfferId equals p.OfferId
                join ph in dc.Photographer on p.PhotographerId equals ph.PhotographerId
                where o.OfferId == id
                select new OfferInfo{ OfferId = o.OfferId, OfferName = o.OfferName, Description = o.Description, PhotographerId = p.PhotographerId
                , PhotographerName=ph.PhotographerName, Price = o.Price, SortOrder = o.SortOrder};
                var res = offer.ToList();
                return res.FirstOrDefault();

            }
        }

        [HttpGet]

        public List<PicInfo> GetOfferPics(long id, long id2)
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.OfferPicture.Where(p => p.OfferId == id && p.OfferPictureId > id2).Take(10).Select(p => new PicInfo { PictureId = p.OfferPictureId, Path = p.Path }).ToList();
            }
        }
    }
}
