using Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostMessage.Responses
{
    public class UpdPhotographerWorkResponse : Response
    {
        public long PhotographerId { get; set; }
        public long PhotographerWorkId { get; set; }
    }
}
