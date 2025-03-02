using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Dabble.Web
{
    /// <summary>
    /// Program's entry point
    /// </summary>
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Dabble";
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostBuilder, configBuilder) => configBuilder
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{hostBuilder.HostingEnvironment.EnvironmentName}.json", true)
                    .AddJsonEnvVar("DABBLE_SETTINGS", true)
                ).UseStartup<Startup>();
    }
}
