using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace GameServer
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");

                var host = new WebHostBuilder()
                    .UseUrls("http://*:1000", "http://0.0.0.0:5000")
                    .UseKestrel()
                    .UseStartup<Startup>()
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

            
        }
    }
}
