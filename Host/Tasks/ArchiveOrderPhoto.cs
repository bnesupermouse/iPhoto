using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HostDB;
using Host.Models;

namespace Host.Tasks
{
    public class ArchiveOrderPhoto
    {
        public static void Processing()
        {
            while(true)
            {
                List<long> ToArchiveOrders = new List<long>();
                using (var dc = new HostDBDataContext())
                {
                    ToArchiveOrders.AddRange(dc.CustomerOrder.Where(o=>o.Status == (int)OrderStatus.OrderFinalised && !o.Archived).Select(o=>o.SerialNo).ToList());
                }
                foreach(var o in ToArchiveOrders)
                {
                    TxArchivePhoto txn = new TxArchivePhoto();
                    txn.OrderId = o;
                    TxnFunc.ProcessTxn(txn);
                }
                Thread.Sleep(200);
            }
        }
    }
}
