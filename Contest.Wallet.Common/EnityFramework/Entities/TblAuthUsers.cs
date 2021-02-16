using Consent.Common.Repository.SQL.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Consent.Common.EnityFramework.Entities
{
    public class TblAuthUsers : IdentityUser
    {
        public TblAuthUsers()
        {
            Id = Guid.NewGuid().ToString();
        }

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
    }
}
