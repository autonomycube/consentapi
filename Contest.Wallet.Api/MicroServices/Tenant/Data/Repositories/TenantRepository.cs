using Consent.Common.Repository.SQL;
using Consent.Api.Tenant.Data.DbContexts;
using Consent.Api.Tenant.Data.Repositories.Abstract;
using Consent.Common.EnityFramework.Entities;

namespace Consent.Api.Tenant.Data.Repositories
{
    public class TenantRepository
        : Repository<TblAuthTenants, string>, ITenantRepository
    {
        public TenantRepository(TenantDbContext context)
            : base(context)
        {
        }
    }
}
