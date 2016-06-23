using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HostDB;
namespace Host
{
    public class UpdOffer:Request
    {
        public UpdOffer()
        {
            TxnId = 2;
            VenueChanged = false;
        }
        public Offer OldOffer { get; set; }
        public Offer NewOffer { get; set; }
        public long PhotographerId { get; set; }
        public List<int> VenueIds { get; set; }
        public bool VenueChanged { get; set; }
        public int Action { get; set; }
    }
}
