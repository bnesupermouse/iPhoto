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
            var path = new PhysicalFileSystem(@"../../WebSrc/app/images/customer/");
            var fileName = Request.Headers.GetValues("X-File-Name").FirstOrDefault();
            var fileSize = Request.Headers.GetValues("X-File-Size").FirstOrDefault();
            var fileType = Request.Headers.GetValues("X-File-Type").FirstOrDefault();
            var orderId = Request.Headers.GetValues("X-Order-Id").FirstOrDefault();
            var orderStatus = Request.Headers.GetValues("X-Order-Status").FirstOrDefault();

            long OrderId = long.Parse(orderId);
            int OrderStatus = int.Parse(orderStatus);
            string root = "";
            if (OrderStatus == (int)Host.OrderStatus.OrderConfirmed || int.Parse(orderStatus) == (int)Host.OrderStatus.RawPhotoUploading)
            {
                root = path.Root + orderId + @"/"+@"raw/";
            }
            else if (int.Parse(orderStatus) == (int)Host.OrderStatus.PhotoSelected || int.Parse(orderStatus) == (int)Host.OrderStatus.RetouchedPhotoUploading || int.Parse(orderStatus) == (int)Host.OrderStatus.RetouchedPhotoConfirming)
            {
                root = path.Root + orderId + @"/" + @"retouched/";
            }
            Directory.CreateDirectory(root);
            var saveToFileLoc = root + fileName;
            var task = this.Request.Content.ReadAsStreamAsync();
            task.Wait();
            Stream requestStream = task.Result;

            try
            {
                Stream fileStream = File.Create(saveToFileLoc);
                requestStream.CopyTo(fileStream);
                fileStream.Close();
                requestStream.Close();

                if (int.Parse(orderStatus) == (int)Host.OrderStatus.OrderConfirmed || int.Parse(orderStatus) == (int)Host.OrderStatus.RawPhotoUploading)
                {
                    UploadRawPhoto upRaw = new UploadRawPhoto();
                    upRaw.OrderId = OrderId;
                    upRaw.RawPhotos = new List<Photo>();
                    upRaw.RawPhotos.Add(new Photo { Path = saveToFileLoc, CustomerOrderId = OrderId, PhotoName = fileName, Retouched = false, Selected = false, Confirmed = false, SortOrder = 0 });
                    TxUploadRawPhoto txn = new TxUploadRawPhoto();
                    txn.request = upRaw;
                    TxnFunc.ProcessTxn(txn);
                }
                else if (int.Parse(orderStatus) == (int)Host.OrderStatus.PhotoSelected || int.Parse(orderStatus) == (int)Host.OrderStatus.RetouchedPhotoUploading || int.Parse(orderStatus) == (int)Host.OrderStatus.RetouchedPhotoConfirming)
                {
                    UploadRetouchedPhoto upRetouched = new UploadRetouchedPhoto();
                    upRetouched.OrderId = OrderId;
                    upRetouched.RetouchedPhotos = new List<Photo>();
                    upRetouched.RetouchedPhotos.Add(new Photo { Path = saveToFileLoc, CustomerOrderId = OrderId, PhotoName = fileName, Retouched = true, Selected = false, Confirmed = false, SortOrder = 0 });
                    TxUploadRetouchedPhoto txn = new TxUploadRetouchedPhoto();
                    txn.request = upRetouched;
                    TxnFunc.ProcessTxn(txn);
                }

                if (int.Parse(orderStatus) == (int)Host.OrderStatus.OrderConfirmed)
                {
                    UpdateOrderStatus updOrder = new Host.UpdateOrderStatus();
                    updOrder.OrderId = long.Parse(orderId);
                    updOrder.ToStatus = (int)Host.OrderStatus.RawPhotoUploading;
                    TxUpdateOrderStatus txn = new TxUpdateOrderStatus();
                    txn.request = updOrder;
                    TxnFunc.ProcessTxn(txn);
                }
                if (int.Parse(orderStatus) == (int)Host.OrderStatus.PhotoSelected)
                {
                    UpdateOrderStatus updOrder = new Host.UpdateOrderStatus();
                    updOrder.OrderId = long.Parse(orderId);
                    updOrder.ToStatus = (int)Host.OrderStatus.RetouchedPhotoUploading;
                    TxUpdateOrderStatus txn = new TxUpdateOrderStatus();
                    txn.request = updOrder;
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
                if(res!=null)
                {
                    if(res.Status <= (int)OrderStatus.OrderConfirmed)
                    {
                        return res;
                    }
                    else
                    {
                        res.RawPhotos = dc.Photo.Where(p => p.CustomerOrderId == id && !p.Retouched).Select(p => p.Path).ToList();
                        res.RetouchedPhotos = dc.Photo.Where(p => p.CustomerOrderId == id && p.Retouched).Select(p => p.Path).ToList();
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
                                     Paid = o.Paid?1:0
                                 };
                    var res =  orders.ToList();
                    return res;
                }
            }
        }
    }
}
