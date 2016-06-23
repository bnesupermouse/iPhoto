using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class UploadRetouchedPhoto : Request
    {
        public UploadRetouchedPhoto()
        {
            TxnId = 1;
            LastPhoto = false;
        }
        public long OrderId { get; set; }
        public List<Photo> RetouchedPhotos { get; set; }
        public bool LastPhoto { get; set; }
    }
}
