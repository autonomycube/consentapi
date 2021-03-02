using Consent.Common.Repository.SQL.Models;
using System;

namespace Consent.Common.EnityFramework.Entities
{
    public partial class TblTenantInvitations : EntityBase<string>
    {
        public TblTenantInvitations()
        {
            Id = Guid.NewGuid().ToString();
        }

        public virtual string Email { get; set; }
        public virtual bool Registered { get; set; }
        public virtual string TenantId { get; set; }
    }
}
