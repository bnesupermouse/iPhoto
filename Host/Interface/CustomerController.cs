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
                return txn.response;
            }
            else
            {
                return null;
            }
        }


        [HttpPost]
        public Response SignOn(Customer ctm)
        {
            CustomerLogin request = new CustomerLogin();
            request.Email = ctm.Email;
            request.Password = ctm.Password;
            TxCustomerLogin txn = new TxCustomerLogin();
            txn.request = request;
            var res = TxnFunc.ProcessTxn(txn);
            if (res == Result.Success)
            {
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
                return dc.Customer.Where(c => c.CustomerId == id).FirstOrDefault();
            }
        }
    }
}
