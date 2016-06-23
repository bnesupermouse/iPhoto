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
            Result res = Result.Success;
            //Stopwatch stopWatchp = new Stopwatch();
            //stopWatchp.Start();
            res = tx.Validate();
            //Console.WriteLine("Validate "+stopWatchp.ElapsedMilliseconds);
            //stopWatchp.Restart();
            if(res != Result.Success)
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
    }
}
