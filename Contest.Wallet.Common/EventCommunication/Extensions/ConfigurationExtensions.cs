using Amazon;
using Contest.Wallet.Common.EventCommunication.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace Contest.Wallet.Common.EventCommunication.Extensions
{
    public static class ConfigurationExtensions
    {
        private static string AwsOptionsKey = "AwsEventOptions";
        public static AwsEventOptions GetAwsEventOptions(this IConfiguration configuration)
        {
            return new AwsEventOptions()
            {
                Region = RegionEndpoint.GetBySystemName(configuration["AWS:Region"]),
                AccessKey = configuration["AWS:AccessKey"],
                SecretKey = configuration["AWS:SecretKey"],
                PublishFailureReAttempts = Convert.ToInt32(configuration[AwsOptionsKey + ":PublishFailureReAttempts"]),
                MessageRetentionSeconds = Convert.ToInt32(configuration[AwsOptionsKey + ":MessageRetentionSeconds"]),
                DeliveryDelaySeconds = Convert.ToInt32(configuration[AwsOptionsKey + ":DeliveryDelaySeconds"]),
                CustomMessageQueue = configuration[AwsOptionsKey + ":CustomMessageQueue"]
            };
        }
    }
}