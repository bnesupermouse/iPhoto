using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HostDB;
namespace Host
{
    public class UpdPhotographerWork:Request
    {
        public UpdPhotographerWork()
        {
            TxnId = 2;
        }
        public PhotographerWork OldPhotographerWork { get; set; }
        public PhotographerWork NewPhotographerWork { get; set; }
        public int Action { get; set; }
    }
}
