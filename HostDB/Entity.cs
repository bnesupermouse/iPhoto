using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.SqlClient;
namespace HostDB
{
    public class Entity : ICloneable
    {
        public string connectionString;
        public Entity()
        {
            connectionString = "Data Source=TA030462;Initial Catalog=iPhoto;Integrated Security=True";
        }
        public object Clone()
        {
            return (Entity)this.MemberwiseClone();
        }

        public virtual Entity Fetch()
        {
            return null;
        }

        public virtual bool Compare(Entity curEntity)
        {
            return true;
        }

        public virtual bool InsertSql()
        {
            return true;
        }

        public virtual bool UpdateSql()
        {
            return true;
        }

        public virtual bool DeleteSql()
        {
            return true;
        }
    }
}
