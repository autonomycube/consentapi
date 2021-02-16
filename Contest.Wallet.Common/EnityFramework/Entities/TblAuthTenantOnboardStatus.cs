using Consent.Common.Repository.SQL.Models;
using System;

namespace Consent.Common.EnityFramework.Entities
{
    public class TblAuthTenantOnboardStatus : EntityBase<string>
    {
        public TblAuthTenantOnboardStatus()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            UpdatedDate = DateTime.UtcNow;
        }

        public virtual string Comment { get; set; }
    }
}
