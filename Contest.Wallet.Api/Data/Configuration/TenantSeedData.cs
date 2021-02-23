using Consent.Common.EnityFramework.Entities;
using Consent.Common.EnityFramework.Entities.Identity;

namespace Consent.Api.Data.Configuration
{
    public class TenantSeedData
    {
        public TblAuthTenants Tenant { get; set; }
        public UserIdentity User { get; set; }
    }
}
