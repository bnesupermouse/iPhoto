using Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostMessage.Responses
{
    public class UpdOfferResponse:Response
    {
        public long OfferId { get; set; }
        public string OfferName { get; set; }
        public long PhotographerId { get; set; }

    }
}
