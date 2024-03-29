﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Consent.Common.EnityFramework.Entities.Identity
{
    public class UserIdentity : IdentityUser<string>
    {
        public UserIdentity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            UpdatedDate = DateTime.UtcNow;
        }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTimeOffset? DateOfBirth { get; set; }
        public virtual string ProfilePicture { get; set; }
        public virtual string Country { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Street { get; set; }
        public virtual string Zip { get; set; }
        public virtual string Gender { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual bool IsKYE { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual string TenantId { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
    }

    public enum UserType
    {
        Tenant = 0,
        Holder = 1
    }
}
