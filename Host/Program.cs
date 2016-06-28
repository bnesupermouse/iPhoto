using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using HostDB;
using System.Reflection;
using HostMessage.Responses;
using System.Threading.Tasks;
using Host.Common;
using Stripe;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Host
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080");

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
            }

            MessageConsumer.StartConsumer();
        }

        static public void ParallelTest()
        {
            try
            {
                var phAddPhResp = TestAddNewPhotographer() as UpdPhotographerResponse;
                var phLoginResp = TestPhotographerLogin(phAddPhResp.Email) as PhotographerLoginResponse;
                var phAddWord = TestAddPhotographerWork(phLoginResp.SessionId) as UpdPhotographerWorkResponse;
                var phWorkAddpic = TestPhotographerWorkUploadPhotos(phLoginResp.SessionId, phAddWord.PhotographerWorkId) as UploadPhotographerWorkPictureResponse;
                var addOfferResp = TestAddNewOffer(phLoginResp.SessionId, phLoginResp.PhotographerId) as UpdOfferResponse;
                var addOfferPhResp = TestOfferUploadPhotos(phLoginResp.SessionId, addOfferResp.OfferId) as UploadOfferPictureResponse;
                var addResp = TestAddNewCustomer() as UpdCustomerResponse;
                var loginResp = TestCustomerLogin(addResp.Email) as CustomerLoginResponse;
                var placeOrderResp = TestPlaceOrder(loginResp.SessionId, addOfferResp.OfferId, loginResp.CustomerId, phLoginResp.PhotographerId) as PlaceOrderResponse;
                var confirmResp = TestConfirmOrder(phLoginResp.SessionId, placeOrderResp.OrderId) as UpdateOrderStatusResponse;
                var uploadResp = TestUploadPhotos(phLoginResp.SessionId, confirmResp.OrderId);
                var uploadedResp = TestConfirmRawPhotoUploaded(phLoginResp.SessionId, confirmResp.OrderId);
                var selectResp = TestSelectPhotos(loginResp.SessionId, confirmResp.OrderId);
                var selectedResp = TestConfirmRawPhotoSelected(loginResp.SessionId, confirmResp.OrderId);
                var uploadingRetouched = TestUploadRetouchedPhotos(phLoginResp.SessionId, confirmResp.OrderId);
                var retouchedUploaded = TestConfirmRetouchedPhotoUploaded(phLoginResp.SessionId, confirmResp.OrderId);
                var confirm = TestConfirmRetouchedPhoto(loginResp.SessionId, confirmResp.OrderId);
                var finalise = TestFinaliseOrder(loginResp.SessionId, confirmResp.OrderId);

            }
            catch (Exception ex)
            {

            }
        }
        static Response TestAddNewPhotographer()
        {
            UpdPhotographer req = new UpdPhotographer();
            Photographer ctm = new Photographer();
            ctm.Email = new Random().Next(1, 10000) + "test902@testc.om";
            ctm.PhotographerName = "photographer";
            ctm.Password = "password";
            req.NewPhotographer = ctm;
            req.Action = 1;
            Stopwatch stopWatch4 = new Stopwatch();
            TxUpdPhotographer txn = new TxUpdPhotographer();
            txn.request = req;
            stopWatch4.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch4.ElapsedMilliseconds);
            stopWatch4.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("Photographer added successfully");
            }
            else
            {
                Console.WriteLine("No Photographer added");
            }
            return txn.response;
        }

        static Response TestPhotographerLogin(string email)
        {
            PhotographerLogin req = new PhotographerLogin();
            req.Email = email;
            req.Password = "password";
            TxPhotographerLogin txn = new TxPhotographerLogin();
            txn.request = req;
            var res = TxnFunc.ProcessTxn(txn);
            if (res == Result.Success)
            {
                Console.WriteLine("Photographer login successfully");
            }
            else
            {
                Console.WriteLine("No photographer login added");
            }
            return txn.response;
        }

        static Response TestAddPhotographerWork(long sessionId)
        {
            UpdPhotographerWork req = new UpdPhotographerWork();
            req.SessionId = sessionId;
            PhotographerWork pw = new PhotographerWork();
            pw.Name = "AwesomeWork";
            pw.PhotoTypeId = 1;
            req.NewPhotographerWork = pw;
            req.Action = 1;
            Stopwatch stopWatch4 = new Stopwatch();
            TxUpdPhotographerWork txn = new TxUpdPhotographerWork();
            txn.request = req;
            stopWatch4.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch4.ElapsedMilliseconds);
            stopWatch4.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("PhotographerWork added successfully");
            }
            else
            {
                Console.WriteLine("No PhotographerWork added");
            }
            return txn.response;
        }

        static Response TestPhotographerWorkUploadPhotos(long sessionId, long photographerWorkId)
        {
            UploadPhotographerWorkPicture req = new UploadPhotographerWorkPicture();
            req.SessionId = sessionId;
            req.PhotographerWorkId = photographerWorkId;
            
            PhotographerWorkPicture ph = new PhotographerWorkPicture();
            ph.PictureName = "test photo";
            ph.Path = @"c:\test";
            ph.PhotographerWorkId = photographerWorkId;
            req.Pictures = new List<PhotographerWorkPicture>();
            req.Pictures.Add(ph);
            Stopwatch stopWatch3 = new Stopwatch();
            TxUploadPhotographerWorkPicture txn = new TxUploadPhotographerWorkPicture();
            txn.request = req;
            stopWatch3.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch3.ElapsedMilliseconds);
            stopWatch3.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("PhotographerWorkPicture uploaded successfully");
            }
            else
            {
                Console.WriteLine("No PhotographerWorkPicture uploaded");
            }
            return txn.response;
        }

        static Response TestAddNewOffer(long sessionId, long photographerId)
        {
            UpdOffer req = new UpdOffer();
            req.SessionId = sessionId;
            req.PhotographerId = photographerId;
            Offer offer = new Offer();
            offer.OfferName = "OfferTest";
            offer.Description = "test";
            offer.Price = 99;
            offer.Status = 0;
            offer.NoCostume = 1;
            offer.NoMakeup = 1;
            offer.NoRawPhoto = 100;
            offer.NoRetouchedPhoto = 20;
            offer.NoServicer = 1;
            offer.NoVenue = 1;
            offer.PhotoTypeId = 1;
            offer.DurationHour = 2;
            req.NewOffer = offer;
            req.Action = 1;
            Stopwatch stopWatch4 = new Stopwatch();
            TxUpdOffer txn = new TxUpdOffer();
            txn.request = req;
            stopWatch4.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch4.ElapsedMilliseconds);
            stopWatch4.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("Offer added successfully");
            }
            else
            {
                Console.WriteLine("No offer added");
            }
            return txn.response;
        }

        static Response TestOfferUploadPhotos(long sessionId, long offerId)
        {
            UploadOfferPicture req = new UploadOfferPicture();
            req.SessionId = sessionId;
            req.OfferId = offerId;
            OfferPicture ph = new OfferPicture();
            ph.PictureName = "test photo";
            ph.Path = @"c:\test";
            ph.OfferId = offerId;
            req.Pictures = new List<OfferPicture>();
            req.Pictures.Add(ph);
            Stopwatch stopWatch3 = new Stopwatch();
            TxUploadOfferPicture txn = new TxUploadOfferPicture();
            txn.request = req;
            stopWatch3.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch3.ElapsedMilliseconds);
            stopWatch3.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("offer picture uploaded successfully");
            }
            else
            {
                Console.WriteLine("No offer picture uploaded");
            }
            return txn.response;
        }

        static Response TestAddNewCustomer()
        {
            UpdCustomer req = new UpdCustomer();
            Customer ctm = new Customer();
            ctm.Email = new Random().Next(1, 10000) + "test902@testc.om";
            ctm.CustomerName = "test";
            ctm.Password = "password";
            req.NewCustomer = ctm;
            req.Action = 1;
            Stopwatch stopWatch4 = new Stopwatch();
            TxUpdCustomer txn = new TxUpdCustomer();
            txn.request = req;
            stopWatch4.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch4.ElapsedMilliseconds);
            stopWatch4.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("Customer added successfully");
            }
            else
            {
                Console.WriteLine("No customer added");
            }
            return txn.response;
        }

        static Response TestCustomerLogin(string email)
        {
            CustomerLogin req = new CustomerLogin();
            req.Email = email;
            req.Password = "password";
            //Stopwatch stopWatch5 = new Stopwatch();

            TxCustomerLogin txn = new TxCustomerLogin();
            txn.request = req;
            var res = TxnFunc.ProcessTxn(txn);
            if (res == Result.Success)
            {
                Console.WriteLine("Customer login successfully");
            }
            else
            {
                Console.WriteLine("No login added");
            }
            return txn.response;
        }

        static Response TestPlaceOrder(long sessionId, long offerId, long customerId, long photographerId)
        {
            PlaceOrder req = new PlaceOrder();
            req.SessionId = sessionId;
            req.OfferId = offerId;
            req.CustomerId = customerId;
            req.PhotographerId = photographerId;
            req.SessionKey = Guid.NewGuid();
            req.TxnId = 1;
            PaymentInfo pay = new PaymentInfo();
            pay.CardNumber = "4012888888881881";
            pay.Year = "2018";
            pay.Month = "10";
            pay.Cvc = "666";
            req.Payment = pay;
            Stopwatch stopWatch1 = new Stopwatch();
            TxPlaceOrder txn = new TxPlaceOrder();
            txn.request = req;
            stopWatch1.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch1.ElapsedMilliseconds);
            stopWatch1.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("Place Order successfully");
            }
            else
            {
                Console.WriteLine("No order placed");
            }
            return txn.response;
        }


        static Response TestConfirmOrder(long sessionId, long orderId)
        {
            UpdateOrderStatus req = new UpdateOrderStatus();
            req.SessionId = sessionId;
            req.OrderId = orderId;
            req.ToStatus = OrderStatus.OrderConfirmed;
            Stopwatch stopWatch2 = new Stopwatch();
            TxUpdateOrderStatus txn = new TxUpdateOrderStatus();
            txn.request = req;
            stopWatch2.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch2.ElapsedMilliseconds);
            stopWatch2.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("Confirm Order successfully");
            }
            else
            {
                Console.WriteLine("No order confirmed");
            }
            return txn.response;
        }

        static Response TestConfirmRawPhotoUploaded(long sessionId, long orderId)
        {
            UpdateOrderStatus req = new UpdateOrderStatus();
            req.SessionId = sessionId;
            req.OrderId = orderId;
            req.ToStatus = OrderStatus.RawPhotoUploaded;
            Stopwatch stopWatch2 = new Stopwatch();
            TxUpdateOrderStatus txn = new TxUpdateOrderStatus();
            txn.request = req;
            stopWatch2.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch2.ElapsedMilliseconds);
            stopWatch2.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("Confirm Raw Photo uploaded successfully");
            }
            else
            {
                Console.WriteLine("No order confirmed");
            }
            return txn.response;
        }

        static Response TestUploadPhotos(long sessionId, long orderId)
        {
            UploadRawPhoto req = new UploadRawPhoto();
            req.SessionId = sessionId;
            req.OrderId = orderId;
            Photo ph = new Photo();
            ph.PhotoName = "test photo";
            ph.Path = @"c:\test";
            ph.Retouched = false;
            req.RawPhotos = new List<Photo>();
            req.RawPhotos.Add(ph);
            Stopwatch stopWatch3 = new Stopwatch();
            TxUploadRawPhoto txn = new TxUploadRawPhoto();
            txn.request = req;
            stopWatch3.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch3.ElapsedMilliseconds);
            stopWatch3.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("photo uploaded successfully");
            }
            else
            {
                Console.WriteLine("No photo uploaded");
            }
            return txn.response;
        }

        static Response TestSelectPhotos(long sessionId, long orderId)
        {
            SelectRawPhoto req = new SelectRawPhoto();
            req.SessionId = sessionId;
            req.OrderId = orderId;
            using (var dc = new HostDBDataContext())
            {
                req.PhotoIds = dc.Photos.Where(p => p.CustomerOrderId == orderId).Select(p=>p.PhotoId).ToList();
            }
            Stopwatch stopWatch3 = new Stopwatch();
            TxSelectRawPhoto txn = new TxSelectRawPhoto();
            txn.request = req;
            stopWatch3.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch3.ElapsedMilliseconds);
            stopWatch3.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("photo selected successfully");
            }
            else
            {
                Console.WriteLine("No photo selected");
            }
            return txn.response;
        }

        static Response TestConfirmRawPhotoSelected(long sessionId, long orderId)
        {
            UpdateOrderStatus req = new UpdateOrderStatus();
            req.SessionId = sessionId;
            req.OrderId = orderId;
            req.ToStatus = OrderStatus.PhotoSelected;
            Stopwatch stopWatch2 = new Stopwatch();
            TxUpdateOrderStatus txn = new TxUpdateOrderStatus();
            txn.request = req;
            stopWatch2.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch2.ElapsedMilliseconds);
            stopWatch2.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("Confirm Raw Photo selected successfully");
            }
            else
            {
                Console.WriteLine("No order confirmed");
            }
            return txn.response;
        }

        static Response TestUploadRetouchedPhotos(long sessionId, long orderId)
        {
            UploadRetouchedPhoto req = new UploadRetouchedPhoto();
            req.SessionId = sessionId;
            req.OrderId = orderId;
            Photo ph = new Photo();
            ph.PhotoName = "test photo";
            ph.Path = @"c:\test";
            ph.Selected = true;
            ph.Retouched = true;
            req.RetouchedPhotos = new List<Photo>();
            req.RetouchedPhotos.Add(ph);
            Stopwatch stopWatch3 = new Stopwatch();
            TxUploadRetouchedPhoto txn = new TxUploadRetouchedPhoto();
            txn.request = req;
            stopWatch3.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch3.ElapsedMilliseconds);
            stopWatch3.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("Retouched photo uploading successfully");
            }
            else
            {
                Console.WriteLine("No photo uploaded");
            }
            return txn.response;
        }

        static Response TestConfirmRetouchedPhotoUploaded(long sessionId, long orderId)
        {
            UpdateOrderStatus req = new UpdateOrderStatus();
            req.SessionId = sessionId;
            req.OrderId = orderId;
            req.ToStatus = OrderStatus.RetouchedPhotoUploaded;
            Stopwatch stopWatch2 = new Stopwatch();
            TxUpdateOrderStatus txn = new TxUpdateOrderStatus();
            txn.request = req;
            stopWatch2.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch2.ElapsedMilliseconds);
            stopWatch2.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("Confirm Retouched Photo uploaded successfully");
            }
            else
            {
                Console.WriteLine("No order confirmed");
            }
            return txn.response;
        }

        static Response TestConfirmRetouchedPhoto(long sessionId, long orderId)
        {
            ConfirmRetouchedPhoto req = new ConfirmRetouchedPhoto();
            req.SessionId = sessionId;
            req.OrderId = orderId;
            using (var dc = new HostDBDataContext())
            {
                req.PhotoIds = dc.Photos.Where(p => p.CustomerOrderId == orderId && p.Selected && p.Retouched).Select(p => p.PhotoId).ToList();
            }
            Stopwatch stopWatch3 = new Stopwatch();
            TxConfirmRetouchedPhoto txn = new TxConfirmRetouchedPhoto();
            txn.request = req;
            stopWatch3.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch3.ElapsedMilliseconds);
            stopWatch3.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("retouched photo confirmed successfully");
            }
            else
            {
                Console.WriteLine("No photo uploaded");
            }
            return txn.response;
        }

        static Response TestFinaliseOrder(long sessionId, long orderId)
        {
            UpdateOrderStatus req = new UpdateOrderStatus();
            req.SessionId = sessionId;
            req.OrderId = orderId;
            req.ToStatus = OrderStatus.OrderFinalised;
            Stopwatch stopWatch2 = new Stopwatch();
            TxUpdateOrderStatus txn = new TxUpdateOrderStatus();
            txn.request = req;
            stopWatch2.Start();
            var res = TxnFunc.ProcessTxn(txn);
            Console.WriteLine("Time elapsed " + stopWatch2.ElapsedMilliseconds);
            stopWatch2.Restart();
            if (res == Result.Success)
            {
                Console.WriteLine("Order finalised successfully");
            }
            else
            {
                Console.WriteLine("No order confirmed");
            }
            return txn.response;
        }
    }
}
