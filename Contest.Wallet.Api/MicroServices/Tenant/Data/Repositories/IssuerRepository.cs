using Contest.Wallet.Common.Repository.SQL;
using Contest.Wallet.Api.Tenant.Data.DbContexts;
using Contest.Wallet.Api.Tenant.Data.Entities;
using Contest.Wallet.Api.Tenant.Data.Repositories.Abstract;

namespace Contest.Wallet.Api.Tenant.Data.Repositories
{
    public class IssuerRepository
        : Repository<TblIssuers, string>, IIssuerRepository
    {
        public IssuerRepository(TenantDbContext context)
            : base(context)
        {
        }
    }
}
