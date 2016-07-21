using Host.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Host
{
    public class TxnFunc
    {
        static private Object thisLock = new Object();

        static public Result ProcessTxn(Tx tx)
        {
            try
            {
                Result res = Result.Success;
                //Stopwatch stopWatchp = new Stopwatch();
                //stopWatchp.Start();
                res = tx.Validate();
                //Console.WriteLine("Validate "+stopWatchp.ElapsedMilliseconds);
                //stopWatchp.Restart();
                if (res != Result.Success)
                {
                    return res;
                }

                lock (thisLock)
                {
                    res = tx.Prepare();
                    if (res != Result.Success)
                    {
                        return res;
                    }
                    //Console.WriteLine("Prepare " + stopWatchp.ElapsedMilliseconds);
                    //stopWatchp.Restart();

                    res = tx.Update();
                    //Console.WriteLine("Update " + stopWatchp.ElapsedMilliseconds);
                    if (res != Result.Success)
                    {
                        return res;
                    }
                    return res;
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(typeof(TxnFunc), "Transaction Failed", Log4NetLevel.Error);
                if(tx.response == null)
                {
                    tx.response = new Response();
                }
                tx.response.ErrorNo = (int)Errors.InvalidRequest;
                tx.response.ErrorMsg = "Invalid Request";
                return Result.Failed;
            }

        }
    }
}
