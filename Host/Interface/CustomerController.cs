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
            TxnFunc.ProcessTxn(txn);
            return txn.response;
        }

        [HttpGet]
        public Customer GetCustomer(int id)
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.Customer.Where(p => p.CustomerId == id).FirstOrDefault();

            }
        }

        [HttpPost]
        public Response UpdateCustomer(UpdCustomer updCustomer)
        {
            TxUpdCustomer txn = new TxUpdCustomer();
            txn.request = updCustomer;
            var res = TxnFunc.ProcessTxn(txn);
            return txn.response;
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
            return txn.response;
        }

    }
}
