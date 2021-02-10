using Amazon;

namespace Consent.Common.ApplicationMonitoring.CloudWatch.Abstraction
{
    public interface ICloudWatchConfiguration
    {
        public string GroupName { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public RegionEndpoint Endpoint { get; set; }
        public bool TraceLog { get; set; }
    }
}
