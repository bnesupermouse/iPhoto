using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Models
{
    public class OrderInfo
    {
        public long SerialNo { get; set; }
        public long OfferId { get; set; }
        public string OfferName { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long PhotographerId { get; set; }
        public string PhotographerName { get; set; }
        public DateTime AppointmentTime { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
    }
}
