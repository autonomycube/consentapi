using Consent.Common.Repository.SQL.Models;

namespace Consent.Common.EnityFramework.Entities
{
    public partial class TblNotifyUserSubscription : EntityBase<string>
    {
        public virtual string TopicId { get; set; }
        public virtual string UserEndpoint { get; set; }
        public virtual string SubscriptionArn { get; set; }
        public virtual bool? IsActive { get; set; }

        public virtual TblNotifyTopic Topic { get; set; }
    }
}
