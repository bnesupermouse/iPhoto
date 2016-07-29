using System;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;
using Topshelf;
using Stripe;
using Host.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            BackgroundTasks.AddTask(AutoOpenCloseOffer.Processing);
            BackgroundTasks.AddTask(ArchiveOrderPhoto.Processing);
            BackgroundTasks.RunTasks();
            StartTopshelf();
        }

        static void StartTopshelf()
        {
            StripeConfiguration.SetApiKey("sk_test_kHY4ReMrtud46mggLvf1lFnh");
            HostFactory.Run(x =>
            {
                x.Service<WebServer>(s =>
                {
                    s.ConstructUsing(name => new WebServer());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Host");
                x.SetDisplayName("Host");
                x.SetServiceName("Host");
            });
        }
    }
}
