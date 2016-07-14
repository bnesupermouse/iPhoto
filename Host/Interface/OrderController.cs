using Host;
using Host.Models;
using HostDB;
using HostMessage.Responses;
using Microsoft.Owin.FileSystems;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace Host
{
    public class OrderController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UploadPhoto()
        {
            //var uriPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            //var path = new Uri(uriPath).LocalPath;
            var path = new PhysicalFileSystem(@"../../WebSrc/app");
            var fileName = Request.Headers.GetValues("X-File-Name").FirstOrDefault();
            var fileSize = Request.Headers.GetValues("X-File-Size").FirstOrDefault();
            var fileType = Request.Headers.GetValues("X-File-Type").FirstOrDefault();
            var orderId = Request.Headers.GetValues("X-Order-Id").FirstOrDefault();
            var orderStatus = Request.Headers.GetValues("X-Order-Status").FirstOrDefault();

            long OrderId = long.Parse(orderId);
            int OrderStatus = int.Parse(orderStatus);
            string root = "";
            bool uploadRaw = int.Parse(orderStatus) < (int)Host.OrderStatus.PhotoSelected && int.Parse(orderStatus) > (int)Host.OrderStatus.OrderPending;
            bool uploadRetouched = int.Parse(orderStatus) < (int)Host.OrderStatus.OrderFinalised && int.Parse(orderStatus) > (int)Host.OrderStatus.RawPhotoUploaded;
            string photoPath = "";
            string photoDir = "";
            if (uploadRaw)
            {
                photoDir = path.Root + @"/images/customer/" + orderId + @"/" + @"raw";
                photoPath = @"/images/customer/" + orderId + @"/" + @"raw/" + fileName;

            }
            else if (uploadRetouched)
            {
                photoDir = path.Root + @"/images/customer/" + orderId + @"/" + @"retouched";
                photoPath = @"/images/customer/" + orderId + @"/" + @"retouched/" + fileName;
            }
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

                if (uploadRaw)
                {
                    UploadRawPhoto upRaw = new UploadRawPhoto();
                    upRaw.OrderId = OrderId;
                    upRaw.RawPhotos = new List<Photo>();
                    upRaw.RawPhotos.Add(new Photo { Path = photoPath, CustomerOrderId = OrderId, PhotoName = fileName, Retouched = false, Selected = false, Confirmed = false, SortOrder = 0 });
                    TxUploadRawPhoto txn = new TxUploadRawPhoto();
                    txn.request = upRaw;
                    TxnFunc.ProcessTxn(txn);
                }
                else if (uploadRetouched)
                {
                    UploadRetouchedPhoto upRetouched = new UploadRetouchedPhoto();
                    upRetouched.OrderId = OrderId;
                    upRetouched.RetouchedPhotos = new List<Photo>();
                    upRetouched.RetouchedPhotos.Add(new Photo { Path = photoPath, CustomerOrderId = OrderId, PhotoName = fileName, Retouched = true, Selected = false, Confirmed = false, SortOrder = 0 });
                    TxUploadRetouchedPhoto txn = new TxUploadRetouchedPhoto();
                    txn.request = upRetouched;
                    TxnFunc.ProcessTxn(txn);
                }
            }
            catch (IOException)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }

        [HttpPost]
        public Response UpdateOrderStatus(UpdateOrderStatus updOrder)
        {
            TxUpdateOrderStatus txn = new TxUpdateOrderStatus();
            txn.request = updOrder;
            var res = TxnFunc.ProcessTxn(txn);
            return txn.response;
        }

        [HttpPost]
        public Response SelectRawPhotos(SelectRawPhoto select)
        {
            TxSelectRawPhoto txn = new TxSelectRawPhoto();
            txn.request = select;
            var res = TxnFunc.ProcessTxn(txn);
            return txn.response;
        }

        [HttpGet]
        public OrderDetails GetOrderDetails(long id)
        {
            using (var dc = new HostDBDataContext())
            {
                var orders = from o in dc.CustomerOrder
                             join ph in dc.Photographer on o.PhotographerId equals ph.PhotographerId
                             join ctm in dc.Customer on o.CustomerId equals ctm.CustomerId
                             join of in dc.Offer on o.OfferId equals of.OfferId
                             where o.SerialNo == id
                             select new OrderDetails
                             {
                                 SerialNo = o.SerialNo,
                                 CustomerId = ctm.CustomerId,
                                 Amount = o.Amount,
                                 AppointmentTime = o.AppointmentTime,
                                 CustomerName = ctm.CustomerName,
                                 OfferId = o.OfferId,
                                 OfferName = of.OfferName,
                                 OrderTime = o.OrderTime,
                                 PhotographerId = ph.PhotographerId,
                                 PhotographerName = ph.PhotographerName,
                                 Status = o.Status,
                                 StatusString = StatusValue.GetStatusValue(o.Status, o.Paid),
                                 LabelString = StatusValue.GetLabelValue(o.Status, o.Paid),
                                 Paid = o.Paid ? 1 : 0
                             };
                OrderDetails res = orders.ToList().FirstOrDefault();
                if (res != null)
                {
                    if (res.Status < (int)OrderStatus.OrderConfirmed)
                    {
                        return res;
                    }
                    else
                    {
                        if (res.Status <= (int)OrderStatus.PhotoSelected)
                        {
                            //res.RawPhotos = dc.Photo.Where(p => p.CustomerOrderId == id && !p.Retouched).Take(10).Select(p => new PhotoInfo { PhotoId = p.PhotoId, PhotoName = p.PhotoName, Path = p.Path, Confirmed = p.Confirmed, Retouched = p.Retouched, Selected = p.Selected }).ToList();
                        }
                        if (res.Status > (int)OrderStatus.PhotoSelected)
                        {
                            //res.RetouchedPhotos = dc.Photo.Where(p => p.CustomerOrderId == id && p.Retouched).Take(10).Select(p => new PhotoInfo { PhotoId = p.PhotoId, PhotoName = p.PhotoName, Path = p.Path, Confirmed = p.Confirmed, Retouched = p.Retouched, Selected = p.Selected }).ToList();
                        }
                        return res;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        [HttpGet]
        public List<OrderInfo> GetOrderList(long id, int id2, int id3)
        {
            Console.WriteLine("GetOrderList: "+id + " : " + id2 + " : " + id3);
            using (var dc = new HostDBDataContext())
            {
                if (id2 == 1)
                {
                    var orders = from o in dc.CustomerOrder
                                 join ph in dc.Photographer on o.PhotographerId equals ph.PhotographerId
                                 join ctm in dc.Customer on o.CustomerId equals ctm.CustomerId
                                 join of in dc.Offer on o.OfferId equals of.OfferId
                                 where o.CustomerId == id //&& o.Status == 0
                                 select new OrderInfo
                                 {
                                     SerialNo = o.SerialNo,
                                     CustomerId = ctm.CustomerId,
                                     Amount = o.Amount,
                                     AppointmentTime = o.AppointmentTime,
                                     CustomerName = ctm.CustomerName,
                                     OfferId = o.OfferId,
                                     OfferName = of.OfferName,
                                     OrderTime = o.OrderTime,
                                     PhotographerId = ph.PhotographerId,
                                     PhotographerName = ph.PhotographerName,
                                     Status = o.Status,
                                     StatusString = StatusValue.GetStatusValue(o.Status, o.Paid),
                                     LabelString = StatusValue.GetLabelValue(o.Status, o.Paid),
                                     Paid = o.Paid ? 1 : 0
                                 };
                    var res = orders.ToList();
                    return res;
                }
                else
                {
                    var orders = from o in dc.CustomerOrder
                                 join ph in dc.Photographer on o.PhotographerId equals ph.PhotographerId
                                 join ctm in dc.Customer on o.CustomerId equals ctm.CustomerId
                                 join of in dc.Offer on o.OfferId equals of.OfferId
                                 where o.PhotographerId == id //&& o.Status == 0
                                 select new OrderInfo
                                 {
                                     SerialNo = o.SerialNo,
                                     CustomerId = ctm.CustomerId,
                                     Amount = o.Amount,
                                     AppointmentTime = o.AppointmentTime,
                                     CustomerName = ctm.CustomerName,
                                     OfferId = o.OfferId,
                                     OfferName = of.OfferName,
                                     OrderTime = o.OrderTime,
                                     PhotographerId = ph.PhotographerId,
                                     PhotographerName = ph.PhotographerName,
                                     Status = o.Status,
                                     StatusString = StatusValue.GetStatusValue(o.Status, o.Paid),
                                     LabelString = StatusValue.GetLabelValue(o.Status, o.Paid),
                                     Paid = o.Paid ? 1 : 0
                                 };
                    var res = orders.ToList();
                    return res;
                }
            }
        }
        [HttpGet]
        public List<PhotoInfo> GetOrderPhotos(long id, int id2, long id3)
        {
            using (var dc = new HostDBDataContext())
            {
                if (id2 == 1)
                {
                    return dc.Photo.Where(p => p.CustomerOrderId == id && !p.Retouched && p.PhotoId > id3).Take(10).Select(p => new PhotoInfo { PhotoId = p.PhotoId, PhotoName = p.PhotoName, Path = p.Path, Confirmed = p.Confirmed, Retouched = p.Retouched, Selected = p.Selected }).ToList();
                }
                else
                {
                    return dc.Photo.Where(p => p.CustomerOrderId == id && p.Retouched && p.PhotoId > id3).Take(10).Select(p => new PhotoInfo { PhotoId = p.PhotoId, PhotoName = p.PhotoName, Path = p.Path, Confirmed = p.Confirmed, Retouched = p.Retouched, Selected = p.Selected }).ToList();
                }
            }
        }
    }
}
