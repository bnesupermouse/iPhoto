using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host.Common
{
    public class PaymentInfo
    {
        public int PaymentType { get; set; }
        public string AccountName { get; set; }
        public string BSB { get; set; }
        public string AccountNo { get; set; }
    }
}
