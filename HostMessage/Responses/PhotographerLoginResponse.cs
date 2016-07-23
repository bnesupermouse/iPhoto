using Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostMessage.Responses
{
    public class PhotographerLoginResponse : Response
    {
        public long PhotographerId { get; set; }
        public string Email { get; set; }
        public long SessionId { get; set; }
        public string SessionKey { get; set; }
        public string PhotographerName { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsVerified { get; set; }
    }
}
