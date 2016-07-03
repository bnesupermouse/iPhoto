using HostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Models
{
    public class NavigationHeader
    {
        public string CustomerName { get; set; }
        public List<PhotoType> PhotoTypes { get; set; }
    }
}
