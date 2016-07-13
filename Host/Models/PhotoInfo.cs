using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Models
{
    public class PhotoInfo
    {
        public long PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string Path { get; set; }
        public bool Selected { get; set; }
        public bool Retouched { get; set; }

        public bool Confirmed { get; set; }
    }
}
