using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HostDB;
using System.Threading.Tasks;

namespace Host
{
    public class DataSet
    {
        public List<UpdEntity> DataList = new List<UpdEntity>();
        public Result AddNew(Entity oldEnt, Entity newEnt)
        {
            UpdEntity updEnt = new UpdEntity();
            updEnt.OldEntity = oldEnt;
            updEnt.NewEntity = newEnt;
            if (oldEnt != null && newEnt != null)
            {
                updEnt.Action = 2;
            }
            else if (oldEnt != null)
            {
                updEnt.Action = 3;
            }
            else if (newEnt != null)
            {
                updEnt.Action = 1;
            }
            else
            {
                updEnt.Action = 0;
            }
            if (updEnt.Action == 0)
            {
                return Result.Failed;
            }
            else
            {
                DataList.Add(updEnt);
                return Result.Success;
            }
        }

        public Result Validate()
        {
            foreach (var ent in DataList)
            {
                var res = ent.Validate();
                if (!res)
                {
                    return Result.Failed;
                }
            }
            return Result.Success;
        }

        public Result Update()
        {
            //var res = true;
            //Parallel.ForEach(DataList, (ent) => {
            //    var curRes = ent.Update();
            //    if (!curRes)
            //    {
            //        res = curRes;
            //    }
            //}
            //);
            //if(!res)
            //{
            //    return Result.Failed;
            //}
            foreach (var ent in DataList)
            {
                var res = ent.Update();
                if (!res)
                {
                    return Result.Failed;
                }
            }
            return Result.Success;
        }

        public List<UpdEntity> GetEntityListByType<T>() where T : class
        {
            List<UpdEntity> res = new List<UpdEntity>();
            foreach(var ent in DataList)
            {
                if(ent.OldEntity is T || ent.NewEntity  is T)
                {
                    res.Add(ent);
                }
            }
            return res;
        }
    }
}
