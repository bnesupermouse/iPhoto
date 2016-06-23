using Host.Common;
using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class PhotographerWithdraw : Request
    {
        public PhotographerWithdraw()
        {
            TxnId = 1;
        }
        public long PhotographerWorkId { get; set; }
        public decimal Amount { get; set; }
        public PaymentInfo CardInfo { get; set; }
    }
}
