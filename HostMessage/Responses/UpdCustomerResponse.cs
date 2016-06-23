using Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostMessage.Responses
{
    public class UpdCustomerResponse:Response
    {
        public long CustomerId { get; set; }
        public string Email { get; set; }
        public long SessionId { get; set; }

    }
}
