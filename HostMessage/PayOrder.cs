using Host.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class PayOrder:Request
    {
        public PayOrder()
        {
            TxnId = 1;
        }
        //public PaymentInfo Payment { get; set; }
        public long CustomerId { get; set; }
        public long OrderId { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string CVC { get; set; }
        public string Name { get; set; }
    }
}
