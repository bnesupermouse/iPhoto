using Host.Models;
using HostDB;
using HostMessage.Responses;
using Microsoft.Owin.FileSystems;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
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

        [HttpPost]
        public Response UpdateOffer(UpdOffer updOffer)
        {
            TxUpdOffer txn = new TxUpdOffer();
            txn.request = updOffer;
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
                , PhotographerName=ph.PhotographerName, Price = o.Price, SortOrder = o.SortOrder
                , AdditionalRetouchPrice = o.AdditionalRetouchPrice, Comment = o.Comment,
                 DurationHour = o.DurationHour, EndTime = o.EndTime, MaxPeople = o.MaxPeople, NoCostume = o.NoCostume,
                 NoMakeup = o.NoMakeup, NoRawPhoto = o.NoRawPhoto, NoRetouchedPhoto = o.NoRetouchedPhoto,
                 NoServicer = o.NoServicer, NoVenue = o.NoVenue, StartTime = o.StartTime, PhotoTypeId = o.PhotoTypeId };
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

        [HttpGet]
        public List<PhotoType> GetPhotoTypes()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.PhotoType.ToList();
            }
        }

        [HttpPost]
        public HttpResponseMessage UploadPhoto()
        {
            //var uriPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            //var path = new Uri(uriPath).LocalPath;
            var path = new PhysicalFileSystem(@"../../WebSrc/app");
            var fileName = Request.Headers.GetValues("X-File-Name").FirstOrDefault();
            var fileSize = Request.Headers.GetValues("X-File-Size").FirstOrDefault();
            var fileType = Request.Headers.GetValues("X-File-Type").FirstOrDefault();
            var offerId = Request.Headers.GetValues("X-Order-Id").FirstOrDefault();

            long OfferId = long.Parse(offerId);
            string root = "";
            string photoPath = "";
            string photoDir = "";
            photoDir = path.Root + @"/images/offer/" + offerId + @"/";
            photoPath = @"/images/offer/" + offerId + @"/" + fileName;
            root = path.Root + photoPath;
            Directory.CreateDirectory(photoDir);
            var saveToFileLoc = root;
            var task = this.Request.Content.ReadAsStreamAsync();
            task.Wait();
            Stream requestStream = task.Result;

            try
            {
                Stream fileStream = File.Create(saveToFileLoc);
                requestStream.CopyTo(fileStream);
                fileStream.Close();
                requestStream.Close();

                CookieHeaderValue cookie = Request.Headers.GetCookies("cid").FirstOrDefault();

                UploadOfferPicture upPic = new UploadOfferPicture();
                upPic.PhotographerId = long.Parse(cookie["cid"].Value);
                upPic.OfferId = OfferId;
                upPic.Pictures = new List<OfferPicture>();
                upPic.Pictures.Add(new OfferPicture { Path = photoPath, OfferId = OfferId, PictureName = fileName, SortOrder = 0 });
                TxUploadOfferPicture txn = new TxUploadOfferPicture();
                txn.request = upPic;
                TxnFunc.ProcessTxn(txn);
               
            }
            catch (IOException)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }

        [HttpGet]
        public List<Offer> GetOfferList()
        {
            CookieHeaderValue cookie = Request.Headers.GetCookies("cid").FirstOrDefault();
            long PhotographerId = long.Parse(cookie["cid"].Value);
            using (var dc = new HostDBDataContext())
            {
                var orders = from op in dc.OfferPhotographer
                             join of in dc.Offer on op.OfferId equals of.OfferId
                             where op.PhotographerId == PhotographerId 
                             select of;
                var res = orders.ToList();
                return res;
            }
        }
    }
}
