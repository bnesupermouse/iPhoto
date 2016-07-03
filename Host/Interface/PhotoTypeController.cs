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
    public class PhotoTypeController : ApiController
    {
        [HttpGet]
       
        public PhotoTypeInfo GetPhotoTypeOffers(int id)
        {
            CookieHeaderValue cookie = Request.Headers.GetCookies("session").FirstOrDefault();
            if (cookie != null)
            {
                string sessionId = cookie["sid"].Value;
            }

            PhotoTypeInfo info = new PhotoTypeInfo();
            using (var dc = new HostDBDataContext())
            {
                var offers = from o in dc.Offers
                join p in dc.OfferPhotographers on o.OfferId equals p.OfferId
                join ph in dc.Photographers on p.PhotographerId equals ph.PhotographerId
                where o.PhotoTypeId == id
                select new OfferInfo{ OfferId = o.OfferId, OfferName = o.OfferName, Description = o.Description, PhotographerId = p.PhotographerId
                , PhotographerName=ph.PhotographerName, Price = o.Price, SortOrder = o.SortOrder};
                var res = offers.ToList();
                foreach(var of in res)
                {
                    var pics = dc.OfferPictures.Where(o => o.OfferId == of.OfferId).Select(o => o.Path).ToList();
                    of.OfferPics = pics;
                }
                var pt = dc.PhotoTypes.Where(p => p.PhotoTypeId == id).FirstOrDefault();
                if(pt!= null)
                {
                    info.PhotoTypeId = pt.PhotoTypeId;
                    info.PhotoTypeName = pt.PhotoTypeName;
                }
                info.OfferList = res;
                return info;

            }
        }
    }
}
