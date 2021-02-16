using Consent.Common.Repository.SQL.Models;

namespace Consent.Common.EnityFramework.Entities
{
    public partial class TblNotifyUserSubscription : EntityBase<string>
    {
        public string TopicId { get; set; }
        public string UserEndpoint { get; set; }
        public string SubscriptionArn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TblNotifyTopic Topic { get; set; }
    }
}
