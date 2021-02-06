using Contest.Wallet.Common.ApplicationMonitoring.CloudWatch.Abstraction;
using Contest.Wallet.Common.ApplicationMonitoring.CloudWatch.Models;
using Contest.Wallet.Common.ApplicationMonitoring.CloudWatch.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Contest.Wallet.Common.ApplicationMonitoring.CloudWatch.Extensions
{
    public static class CloudWatchLoggerExtensions
    {
        public static IServiceCollection AddCloudWatchLogger(this IServiceCollection services, CloudWatchConfiguration options)
        {
            services.AddSingleton<ICloudWatchConfiguration>(options);
            services.AddSingleton(typeof(ICloudWatchLogger<>), typeof(CloudWatchLogger<>));

            return services;
        }
    }
}