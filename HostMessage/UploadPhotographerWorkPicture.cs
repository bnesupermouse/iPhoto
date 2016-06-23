using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class UploadPhotographerWorkPicture : Request
    {
        public UploadPhotographerWorkPicture()
        {
            TxnId = 1;
        }
        public long PhotographerWorkId { get; set; }
        public List<PhotographerWorkPicture> Pictures { get; set; }
    }
}
