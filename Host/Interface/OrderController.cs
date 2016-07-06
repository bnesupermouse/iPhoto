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
    public class OrderController : ApiController
    {
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
                                     StatusString = StatusValue.GetStatusValue(o.Status),
                                     LabelString = StatusValue.GetLabelValue(o.Status)
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
                                     StatusString = StatusValue.GetStatusValue(o.Status),
                                     LabelString = StatusValue.GetLabelValue(o.Status)
                                 };
                    var res =  orders.ToList();
                    return res;
                }
            }
        }
    }
}
