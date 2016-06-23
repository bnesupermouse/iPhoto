using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostDB
{
    public class UpdEntity
    {
        public Entity OldEntity;
        public Entity NewEntity;
        public int Action;

        public bool Validate()
        {
            if (Action == 1)
            {
                if (NewEntity != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Action == 2)
            {
                if (OldEntity != null && NewEntity != null)
                {
                    var curEntity = OldEntity.Fetch();
                    if(curEntity != null)
                    {
                        return OldEntity.Compare(curEntity);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (Action == 3)
            {
                if (OldEntity != null)
                {
                    var curEntity = OldEntity.Fetch();
                    if (curEntity != null)
                    {
                        return OldEntity.Compare(curEntity);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool Update()
        {
            if (Action == 1)
            {
                if (NewEntity != null)
                {
                    try
                    {
                        return NewEntity.InsertSql();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (Action == 2)
            {
                if (OldEntity != null && NewEntity != null)
                {
                    try
                    {
                        return NewEntity.UpdateSql();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (Action == 3)
            {
                if (OldEntity != null)
                {
                    try
                    {
                        return OldEntity.DeleteSql();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
