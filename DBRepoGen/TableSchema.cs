using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemDBGenerator
{
    public class TableSchema
    {
        public string TableName = string.Empty;
        public List<string> PrimaryKeys = new List<string>();
        public List<string> Columns = new List<string>();
    }
}
