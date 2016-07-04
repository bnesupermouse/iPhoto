using Host.Models;
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
        public MainPageContent Index()
        {
            MainPageContent header = new MainPageContent();
            using (var dc = new HostDBDataContext())
            {
                header.PhotoTypes = dc.PhotoTypes.ToList();
            }
            return header;
        }
    }
}
