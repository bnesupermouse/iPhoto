using HostDB;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Host
{
    public class CustomerController : ApiController
    {
        
        [HttpGet]
        string GetMe(int id)
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.Customers.Where(c => c.CustomerId == id).FirstOrDefault().CustomerName;
            }
        }
    }
}
