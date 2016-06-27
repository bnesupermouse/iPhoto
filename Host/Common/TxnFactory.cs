using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Common
{
    public class TxnFactory
    {
        public static Tx CreateTxn(int txnId)
        {
            switch(txnId)
            {
                case 1:
                    return new TxUpdCustomer();
                default:
                    return null;
            }
        }
    }
}
