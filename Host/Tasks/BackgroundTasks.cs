using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Host.Tasks
{
    public class BackgroundTasks
    {
        static public List<Thread> Tasks = new List<Thread>();
        static public void AddTask(ThreadStart func)
        {
            Thread t = new Thread(func);
            Tasks.Add(t);
        }
        static public void RunTasks()
        {
            foreach(var t in Tasks)
            {
                t.IsBackground = true;
                t.Start();
            }
        }
    }
}
