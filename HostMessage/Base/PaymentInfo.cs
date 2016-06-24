using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host.Common
{
    public class PaymentInfo
    {
        public int PaymentType { get; set; }
        public string Name { get; set; }
        public string Cvc { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public int Amount { get; set; }
    }
}
