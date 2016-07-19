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
    public class MainPageController : ApiController
    {
        [HttpGet]
        public MainPageContent Index()
        {
            MainPageContent mainPage = new MainPageContent();
            using (var dc = new HostDBDataContext())
            {
                mainPage.PhotoTypes = dc.PhotoType.ToList();
                mainPage.PhotoTypeOffers = new List<PhotoTypeOffer>();
                foreach(var ph in mainPage.PhotoTypes)
                {
                    PhotoTypeOffer po = new PhotoTypeOffer();
                    po.PhotoTypeId = ph.PhotoTypeId;
                    po.PhotoTypeName = ph.PhotoTypeName;
                    var offers = from o in dc.Offer
                                 where o.PhotoTypeId == ph.PhotoTypeId
                                 select new OfferInfo
                                 {
                                     OfferId = o.OfferId,
                                     OfferName = o.OfferName,
                                     Description = o.Description,
                                     //PhotographerId = ph.PhotographerId,
                                     //PhotographerName = ph.PhotographerName,
                                     Price = o.Price,
                                     SortOrder = o.SortOrder
                                 };
                    var poffers = offers.Take(8).ToList();
                    foreach (var of in poffers)
                    {
                        var pics = dc.OfferPicture.Where(o => o.OfferId == of.OfferId).Select(o => new PicInfo { PictureId = o.OfferPictureId, Path = o.Path }).ToList();
                        of.OfferPics = pics;
                    }
                    po.Offers = poffers;
                    mainPage.PhotoTypeOffers.Add(po);
                }
            }
            return mainPage;
        }
    }
}
