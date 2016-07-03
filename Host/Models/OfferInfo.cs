using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Models
{
    public class OfferInfo
    {
        public long OfferId { get; set; }
        public string OfferName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> OfferPics { get; set; }
        public long PhotographerId { get; set; }
        public string PhotographerName { get; set; }
        public int SortOrder { get; set; }

    }
}
