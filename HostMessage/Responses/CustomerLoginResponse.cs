using Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostMessage.Responses
{
    public class CustomerLoginResponse:Response
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public long SessionId { get; set; }
        public string SessionKey { get; set; }

    }
}
