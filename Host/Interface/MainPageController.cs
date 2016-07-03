using HostDB;
using HostMessage.Responses;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Host
{
    public class MainPageController : ApiController
    {
        [HttpGet]
        public string Index()
        {
            return "Hello";
        }
    }
}
