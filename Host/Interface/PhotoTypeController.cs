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
       
        public PhotoTypeInfo GetPhotoTypeOffers(int id, int id2, int id3)
        {
            CookieHeaderValue cookie = Request.Headers.GetCookies("sid").FirstOrDefault();
            if (cookie != null)
            {
                string sessionId = cookie["sid"].Value;
            }

            PhotoTypeInfo info = new PhotoTypeInfo();
            using (var dc = new HostDBDataContext())
            {
                int min = 0;
                int max = 100000;
                if(id2 > 0&& id3 > 0)
                {
                    min = id2;
                    max = id3;
                }
                var offers = from o in dc.Offer
                join p in dc.OfferPhotographer on o.OfferId equals p.OfferId
                join ph in dc.Photographer on p.PhotographerId equals ph.PhotographerId
                where o.PhotoTypeId == id
                select new OfferInfo{ OfferId = o.OfferId, OfferName = o.OfferName, Description = o.Description, PhotographerId = p.PhotographerId
                , PhotographerName=ph.PhotographerName, Price = o.Price, SortOrder = o.SortOrder};
                var res = offers.Where(o=>o.Price >= min && o.Price<max).ToList();
                foreach(var of in res)
                {
                    var pics = dc.OfferPicture.Where(o => o.OfferId == of.OfferId).Select(o => new PicInfo { PictureId = o.OfferPictureId, Path = o.Path }).ToList();
                    of.OfferPics = pics;
                }
                var pt = dc.PhotoType.Where(p => p.PhotoTypeId == id).FirstOrDefault();
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
