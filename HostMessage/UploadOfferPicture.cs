using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class UploadOfferPicture : Request
    {
        public UploadOfferPicture()
        {
            TxnId = 1;
        }
        public long OfferId { get; set; }
        public long PhotographerId { get; set; }
        public List<OfferPicture> Pictures { get; set; }
    }
}
