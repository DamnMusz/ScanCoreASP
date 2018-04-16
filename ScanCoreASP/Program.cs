using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Saraff.Twain;
using System;

namespace ScanCoreASP
{
    public class Startup
    {
        public Twain32 _twain = new Twain32();

        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure

            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world");
            });
        }

        public void Scan()
        {
            try
            {
                this._twain.Acquire();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void _twain_AcquireCompleted(object sender, EventArgs e)
        {
            try
            {
                string status = "FAIL";
                if (this._twain.ImageCount > 0)
                {
                    status = "OK";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SAMPLE1", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
              .UseKestrel()
              .UseStartup<Startup>()
              .Build();

            host.Run();
        }
    }
}