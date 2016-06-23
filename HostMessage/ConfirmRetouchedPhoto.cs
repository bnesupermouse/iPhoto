using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class ConfirmRetouchedPhoto : Request
    {
        public ConfirmRetouchedPhoto()
        {
            TxnId = 1;
            LastPhoto = false;
        }
        public long OrderId { get; set; }
        public List<long> PhotoIds { get; set; }
        public bool LastPhoto { get; set; }
    }
}
