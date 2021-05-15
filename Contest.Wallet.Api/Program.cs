using Consent.Api.Data.Helpers;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Common.ApplicationMonitoring.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Consent.Api
{
    public class Program
    {
        private const string SeedArgs = "/seed";

        public static async Task Main(string[] args)
        {
            var seed = args.Any(x => x == SeedArgs);
            if (seed) args = args.Except(new[] { SeedArgs }).ToArray();

            var builder = CreateHostBuilder(args).Build();
            var logger = builder.Services.GetService<ILogger<Program>>();
            if (seed)
            {
                try
                {
                    await DbMigrationHelpers.EnsureSeedData(builder);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.LogError("Seed data generation failed.  Error: " + ex.Message);
                }
            }

            try
            {
                logger.LogInfo("Starting web host");
                builder.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.LogError("Host unexpectedly terminated. Error: {0}", ex.StackTrace);
            }
        }

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:8080");
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var settings = env == null ? "appsettings.json" : $"appsettings.{env}.json";
                    config.AddJsonFile(settings, optional: true, reloadOnChange: true);
                    config.AddJsonFile("notificationdata.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile("tenantdata.json", optional: true, reloadOnChange: true);
                }).UseCloudWatchLog();
    }
}
