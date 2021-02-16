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

        public string Name { get; set; }
        public string Arn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<TblNotifyUserSubscription> TblNotfyUsersubscription { get; set; }

    }
}
