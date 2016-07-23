using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HostDB;
namespace Host
{
    public class UpdPhotographer:Request
    {
        public UpdPhotographer()
        {
            TxnId = 2;
        }
        public Photographer OldPhotographer { get; set; }
        public Photographer NewPhotographer { get; set; }
        public int Action { get; set; }
        public long PhotographerId { get; set; }
    }
}
