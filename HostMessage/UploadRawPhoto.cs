using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class UploadRawPhoto : Request
    {
        public UploadRawPhoto()
        {
            TxnId = 1;
        }
        public long OrderId { get; set; }
        public List<Photo> RawPhotos { get; set; }
    }
}
