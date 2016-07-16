using Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostMessage.Responses
{
    public class UpdateOrderStatusResponse:Response
    {
        public long OrderId { get; set; }
        public int Status { get; set; }
        public string StatusString { get; set; }
        public string LabelString { get; set; }
    }
}
