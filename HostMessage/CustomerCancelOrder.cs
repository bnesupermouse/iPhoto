using Host.Common;
using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class CustomerCancelOrder : Request
    {
        public CustomerCancelOrder()
        {
            TxnId = 1;
        }
        public long OrderId { get; set; }
        public PaymentInfo CardInfo { get; set; }
    }
}
