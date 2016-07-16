using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class SelectPhoto : Request
    {
        public SelectPhoto()
        {
            TxnId = 1;
        }
        public long OrderId { get; set; }
        public List<long> SelectedPhotoIds { get; set; }
        public List<long> DeselectedPhotoIds { get; set; }
    }
}
