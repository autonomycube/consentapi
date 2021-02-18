using Consent.Common.Repository.SQL.Models;
using System.Collections.Generic;

namespace Consent.Common.EnityFramework.Entities
{
    public partial class TblNotifyTopic : EntityBase<string>
    {
        public TblNotifyTopic()
        {
            TblNotfyUsersubscription = new HashSet<TblNotifyUserSubscription>();
        }

        public virtual string Name { get; set; }
        public virtual string Arn { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual string TenantId { get; set; }

        public virtual ICollection<TblNotifyUserSubscription> TblNotfyUsersubscription { get; set; }

    }
}
