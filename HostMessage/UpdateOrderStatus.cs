using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class UpdateOrderStatus : Request
    {
        public UpdateOrderStatus()
        {
            TxnId = 1;
        }
        public long OrderId { get; set; }

        public OrderStatus ToStatus { get; set; }
    }
}
