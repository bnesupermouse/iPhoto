using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Host
{
    public enum Result { Success, Failed };

    public class Request
    {
        public long SessionId { get; set; }
        public Guid SessionKey { get; set; }
        public int TxnId { get; set; }
    }
}
