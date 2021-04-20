using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PaymentGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                        options.Listen(IPAddress.Any, 5001, listenOptions =>
                        {
                            var cert = GetHttpCertificates(context.Configuration);
                            listenOptions.UseHttps(cert);
                        });
                    });
                });

        public static X509Certificate2 GetHttpCertificates(IConfiguration config)
        {
            var pfxPath = config.GetValue("PFX_PATH", string.Empty);
            var pfxPass = config.GetValue("PFX_PASS", string.Empty);

            Console.Write($"Certificate path {pfxPath} - ");
            Console.WriteLine(File.Exists(pfxPath) ? "Found" : "Missing");
            Console.WriteLine($"Certificate password {pfxPass}");

            return new X509Certificate2(pfxPath, pfxPass, X509KeyStorageFlags.UserKeySet);
        }
    }
}
