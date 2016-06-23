using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class RemovePhotographerWorkPicture : Request
    {
        public RemovePhotographerWorkPicture()
        {
            TxnId = 1;
        }
        public long PhotographerWorkId { get; set; }
        public List<long> Pictures { get; set; }
    }
}
