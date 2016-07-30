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
                List<long> ToArchiveRawOrders = new List<long>();
                using (var dc = new HostDBDataContext())
                {
                    ToArchiveRawOrders.AddRange(dc.CustomerOrder.Where(o=>o.Status >= (int)OrderStatus.RawPhotoUploaded && o.Status <= (int)OrderStatus.OrderFinalised && !o.RawArchived).Select(o=>o.SerialNo).ToList());
                }
                foreach(var o in ToArchiveRawOrders)
                {
                    TxArchivePhoto txn = new TxArchivePhoto();
                    txn.OrderId = o;
                    txn.ArchiveRaw = true;
                    TxnFunc.ProcessTxn(txn);
                }

                List<long> ToArchiveRetouchedOrders = new List<long>();
                using (var dc = new HostDBDataContext())
                {
                    ToArchiveRetouchedOrders.AddRange(dc.CustomerOrder.Where(o => o.Status == (int)OrderStatus.OrderFinalised && !o.RetouchedArchived).Select(o => o.SerialNo).ToList());
                }
                foreach (var o in ToArchiveRetouchedOrders)
                {
                    TxArchivePhoto txn = new TxArchivePhoto();
                    txn.OrderId = o;
                    txn.ArchiveRaw = false;
                    TxnFunc.ProcessTxn(txn);
                }
                Thread.Sleep(200);
            }
        }
    }
}
