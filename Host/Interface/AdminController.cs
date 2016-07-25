using Host.Models;
using HostDB;
using HostMessage.Responses;
using Microsoft.Owin.FileSystems;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Host
{
    public class AdminController : ApiController
    {


        [HttpPost]
        public Response UpdatePhotographer(UpdPhotographer updPhotographer)
        {
            TxUpdPhotographer txn = new TxUpdPhotographer();
            txn.request = updPhotographer;
            var res = TxnFunc.ProcessTxn(txn);
            return txn.response;
        }

        [HttpGet]
        public Photographer GetPhotographer(int id)
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.Photographer.Where(p => p.PhotographerId == id).FirstOrDefault();

            }
        }

        [HttpGet]
        public List<Photographer> GetPhotographerList(int id)
        {
            using (var dc = new HostDBDataContext())
            {
                if (id == -1)
                {
                    return dc.Photographer.Where(p=>!p.Admin).ToList();
                }
                else
                {
                    return dc.Photographer.Where(p => p.Status == id && !p.Admin).ToList();
                }
            }
        }
    }
}
