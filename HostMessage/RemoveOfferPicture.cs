using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class RemoveOfferPicture : Request
    {
        public RemoveOfferPicture()
        {
            TxnId = 1;
        }
        public long OfferId { get; set; }
        public List<long> Pictures { get; set; }
    }
}
