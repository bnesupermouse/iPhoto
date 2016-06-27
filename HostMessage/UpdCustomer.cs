using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HostDB;
namespace Host
{
    public class UpdCustomer:Request
    {
        public UpdCustomer()
        {
            
        }
        public Customer OldCustomer { get; set; }
        public Customer NewCustomer { get; set; }
        public int Action { get; set; }
    }
}
