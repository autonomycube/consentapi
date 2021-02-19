using Microsoft.AspNetCore.Identity;
using System;

namespace Consent.Common.EnityFramework.Entities.Identity
{
    public class UserIdentity : IdentityUser<string>
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual string ProfilePicture { get; set; }
        public virtual string Country { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Street { get; set; }
        public virtual string Zip { get; set; }
        public virtual string Gender { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual string TenantId { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
    }
}
