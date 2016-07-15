using Host.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class PlaceOrder:Request
    {
        public PlaceOrder()
        {
            TxnId = 1;
        }
        //public PaymentInfo Payment { get; set; }
        public long CustomerId { get; set; }
        public long OfferId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
