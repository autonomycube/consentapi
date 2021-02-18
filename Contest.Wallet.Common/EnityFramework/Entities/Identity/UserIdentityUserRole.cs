using Microsoft.AspNetCore.Identity;
using System;

namespace Consent.Common.EnityFramework.Entities.Identity
{
    public class UserIdentityUserRole : IdentityUserRole<string>
    {
        public virtual bool? IsActive { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
    }
}
