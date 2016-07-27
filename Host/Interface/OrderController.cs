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

            using (var dc = new HostDBDataContext())
            {
                var order = dc.CustomerOrder.Where(o => o.SerialNo == OrderId).FirstOrDefault();
                if(order!= null && !order.Paid)
                {
                    HttpResponseMessage error = new HttpResponseMessage();
                    error.StatusCode = HttpStatusCode.PaymentRequired;
                    return error;
                }
            }
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
                    upRetouched.RetouchedPhotos.Add(new Photo { Path = photoPath, CustomerOrderId = OrderId, PhotoName = fileName, Retouched = true, Selected = true, Confirmed = false, SortOrder = 0 });
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
        public Response SelectRawPhotos(SelectPhoto select)
        {
            TxSelectRawPhoto txn = new TxSelectRawPhoto();
            txn.request = select;
            var res = TxnFunc.ProcessTxn(txn);
            return txn.response;
        }

        [HttpPost]
        public Response SelectRetouchedPhotos(SelectPhoto select)
        {
            TxConfirmRetouchedPhoto txn = new TxConfirmRetouchedPhoto();
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
                                 AppointmentTime = o.AppointmentTime.ToLocalTime(),
                                 CustomerName = ctm.CustomerName,
                                 Phone = ctm.Phone,
                                 OfferId = o.OfferId,
                                 OfferName = of.OfferName,
                                 OrderTime = o.OrderTime.ToLocalTime(),
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
                    DateTime start = res.AppointmentTime.Date;
                    DateTime end = start.AddDays(1);
                    TimeZone zone = TimeZone.CurrentTimeZone;
                    var offset = zone.GetUtcOffset(DateTime.Now);
                    int hours = offset.Hours;
                    var events = dc.CustomerOrder.Where(o => o.PhotographerId == res.PhotographerId && o.AppointmentTime >= start && o.AppointmentTime <= end).Select(o => new Appointment { start = o.AppointmentTime.AddHours(hours), end = o.AppointmentTime.AddHours(hours+2), id = o.SerialNo, text = "OrderId: " + o.SerialNo + " OfferId: " + o.OfferId }).ToList();
                    res.Events = new List<Appointment>();
                    res.Events.AddRange(events);

                    var offer = dc.Offer.Where(o => o.OfferId == res.OfferId).Select(o=>new OfferInfo {
                        OfferId = o.OfferId,
                        OfferName = o.OfferName,
                        Description = o.Description,
                        PhotographerId = res.PhotographerId,
                        PhotographerName = res.PhotographerName,
                        Price = o.Price,
                        SortOrder = o.SortOrder,
                        AdditionalRetouchPrice = o.AdditionalRetouchPrice,
                        Comment = o.Comment,
                        DurationHour = o.DurationHour,
                        EndTime = o.EndTime,
                        MaxPeople = o.MaxPeople,
                        NoCostume = o.NoCostume,
                        NoMakeup = o.NoMakeup,
                        NoRawPhoto = o.NoRawPhoto,
                        NoRetouchedPhoto = o.NoRetouchedPhoto,
                        NoServicer = o.NoServicer,
                        NoVenue = o.NoVenue,
                        StartTime = o.StartTime,
                        PhotoTypeId = o.PhotoTypeId,
                        Status = o.Status
                    }).FirstOrDefault();
                    res.OfferInfo = offer;
                    return res;
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
            using (var dc = new HostDBDataContext())
            {
                if (id2 == 1)
                {
                    if (id3 == -1)
                    {
                        var orders = from o in dc.CustomerOrder
                                     join ph in dc.Photographer on o.PhotographerId equals ph.PhotographerId
                                     join ctm in dc.Customer on o.CustomerId equals ctm.CustomerId
                                     join of in dc.Offer on o.OfferId equals of.OfferId
                                     where o.CustomerId == id
                                     select new OrderInfo
                                     {
                                         SerialNo = o.SerialNo,
                                         CustomerId = ctm.CustomerId,
                                         Amount = o.Amount,
                                         AppointmentTime = o.AppointmentTime.ToLocalTime(),
                                         CustomerName = ctm.CustomerName,
                                         OfferId = o.OfferId,
                                         OfferName = of.OfferName,
                                         OrderTime = o.OrderTime.ToLocalTime(),
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
                                     where o.CustomerId == id && o.Status == id3
                                     select new OrderInfo
                                     {
                                         SerialNo = o.SerialNo,
                                         CustomerId = ctm.CustomerId,
                                         Amount = o.Amount,
                                         AppointmentTime = o.AppointmentTime.ToLocalTime(),
                                         CustomerName = ctm.CustomerName,
                                         OfferId = o.OfferId,
                                         OfferName = of.OfferName,
                                         OrderTime = o.OrderTime.ToLocalTime(),
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
                else
                {
                    if (id3 == -1)
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
                                         AppointmentTime = o.AppointmentTime.ToLocalTime(),
                                         CustomerName = ctm.CustomerName,
                                         OfferId = o.OfferId,
                                         OfferName = of.OfferName,
                                         OrderTime = o.OrderTime.ToLocalTime(),
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
                                     where o.PhotographerId == id && o.Status == id3
                                     select new OrderInfo
                                     {
                                         SerialNo = o.SerialNo,
                                         CustomerId = ctm.CustomerId,
                                         Amount = o.Amount,
                                         AppointmentTime = o.AppointmentTime.ToLocalTime(),
                                         CustomerName = ctm.CustomerName,
                                         OfferId = o.OfferId,
                                         OfferName = of.OfferName,
                                         OrderTime = o.OrderTime.ToLocalTime(),
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
                    var res =  dc.Photo.Where(p => p.CustomerOrderId == id && p.Retouched && p.PhotoId > id3).Take(10).Select(p => new PhotoInfo { PhotoId = p.PhotoId, PhotoName = p.PhotoName, Path = p.Path, Confirmed = p.Confirmed, Retouched = p.Retouched, Selected = p.Selected }).ToList();
                    return res;
                }
            }
        }
    }
}
