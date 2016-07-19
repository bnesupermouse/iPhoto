﻿using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Models
{
    public class PhotoTypeOffer
    {
        public long PhotoTypeId { get; set; }
        public string PhotoTypeName { get; set; }
        public List<OfferInfo> Offers { get; set; }
    }
}
