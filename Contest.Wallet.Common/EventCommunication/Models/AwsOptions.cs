using Amazon;

namespace Contest.Wallet.Common.EventCommunication.Models
{
    public class AwsEventOptions
    {
        public RegionEndpoint Region { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public int PublishFailureReAttempts { get; set; }
        public int MessageRetentionSeconds { get; set; }
        public int DeliveryDelaySeconds { get; set; }
        public string CustomMessageQueue { get; internal set; }
    }
}
