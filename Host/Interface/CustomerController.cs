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
    public class CustomerController : ApiController
    {
        [HttpPost]
        public Response NewAccount(Customer ctm)
        {
            UpdCustomer request = new UpdCustomer();
            request.NewCustomer = ctm;
            request.Action = 1;
            TxUpdCustomer txn = new TxUpdCustomer();
            txn.request = request;
            var res = TxnFunc.ProcessTxn(txn);
            if (res == Result.Success)
            {
                //var resp = new APIResponse();
                //var nv = new NameValueCollection();
                //nv["sid"] = (txn.response as UpdCustomerResponse).SessionId.ToString();
                //nv["skey"] = (txn.response as UpdCustomerResponse).SessionKey.ToString();
                //nv["sname"] = ctm.CustomerName;
                //var cookie = new CookieHeaderValue("session", nv);

                //resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                //resp.response = txn.response;

                //return resp;


                return txn.response;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public Customer GetMe(int id)
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            }
        }
    }
}
