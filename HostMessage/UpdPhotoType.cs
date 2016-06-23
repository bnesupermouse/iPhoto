using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HostDB;
namespace Host
{
    public class UpdPhotoType:Request
    {
        public UpdPhotoType()
        {
            TxnId = 2;
        }
        public PhotoType OldPhotoType { get; set; }
        public PhotoType NewPhotoType { get; set; }
        public int Action { get; set; }
    }
}
