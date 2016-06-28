using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Host
{
    public class APIResponse: HttpResponseMessage
    {
        public Response response { get; set; }
    }
}
