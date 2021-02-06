using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Contest.Wallet.Common.ApplicationMonitoring.Extensions
{
    public static class MonitoringExtensions
    {
        public static IHostBuilder UseCloudWatchLog(this IHostBuilder builder)
        {
            //options for nlog
            NLogAspNetCoreOptions options = new NLogAspNetCoreOptions
            {
                IncludeScopes = false,
            };

            return builder
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);
                    logging.AddFilter("Microsoft", LogLevel.None);
                    logging.AddFilter("Contest.Wallet.Common.CorrelationId", LogLevel.None);
                }).UseNLog(options);
        }
    }
}
