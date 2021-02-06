using Amazon;
using Contest.Wallet.Common.ApplicationMonitoring.CloudWatch.Abstraction;
using Microsoft.Extensions.Configuration;
using System;

namespace Contest.Wallet.Common.ApplicationMonitoring.CloudWatch.Models
{
    public class CloudWatchConfiguration : ICloudWatchConfiguration
    {
        private readonly string CloudWatchKey = "CloudWatchLog";
        public CloudWatchConfiguration(IConfiguration config)
        {
            Endpoint = RegionEndpoint.GetBySystemName(config["AWS:Region"]);
            AccessKey = config["AWS:AccessKey"];
            SecretKey = config["AWS:SecretKey"];
            GroupName = config[CloudWatchKey + ":GroupName"];
            TraceLog = Convert.ToBoolean(config[CloudWatchKey + ":TraceLog"]);
        }

        public string GroupName { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public RegionEndpoint Endpoint { get; set; }
        public bool TraceLog { get; set; }
    }
}
