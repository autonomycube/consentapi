using Consent.Common.Repository.SQL.Models;
using System;

namespace Consent.Common.EnityFramework.Entities
{
    public class TblAuthTenants : EntityBase<string>
    {
        public TblAuthTenants()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            UpdatedDate = DateTime.UtcNow;
        }

        public virtual string CIN { get; set; }
        public virtual string Name { get; set; }
        public virtual string NormalizedName { get; set; }
        public virtual string ProfilePicture { get; set; }
        public virtual string Country { get; set; }
        public virtual string Address { get; set; }
        public virtual TenantType TenantType { get; set; }
        public virtual TenantStatus TenantStatus { get; set; } = TenantStatus.Registered;
        public virtual string Contact { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual int EmployeesCount { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual string TenantId { get; set; }
    }

    public enum TenantType
    {
        Startup = 0,
        BGV = 1,
        RA = 2
    }

    public enum TenantStatus
    {
        Registered = 0,
        OnboardProcessing = 1,
        OnboardComplete = 2,
        OnboardRejected = 3
    }
}
