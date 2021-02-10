using Consent.Common.Repository.SQL;
using Consent.Api.Tenant.Data.DbContexts;
using Consent.Api.Tenant.Data.Entities;
using Consent.Api.Tenant.Data.Repositories.Abstract;

namespace Consent.Api.Tenant.Data.Repositories
{
    public class TenantRepository
        : Repository<TblTenants, string>, ITenantRepository
    {
        public TenantRepository(TenantDbContext context)
            : base(context)
        {
        }
    }
}
