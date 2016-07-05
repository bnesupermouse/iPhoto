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
    public class PhotographerController : ApiController
    {
        [HttpPost]
        public Response NewAccount(Photographer phg)
        {
            UpdPhotographer request = new UpdPhotographer();
            request.NewPhotographer = phg;
            request.Action = 1;
            TxUpdPhotographer txn = new TxUpdPhotographer();
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
        public Response SignOn(Photographer phg)
        {
            PhotographerLogin request = new PhotographerLogin();
            request.Email = phg.Email;
            request.Password = phg.Password;
            TxPhotographerLogin txn = new TxPhotographerLogin();
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
    }
}
