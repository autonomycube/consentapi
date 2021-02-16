using Consent.Common.Repository.SQL.Abstract;
using Consent.Common.EnityFramework.Entities;

namespace Consent.Api.Tenant.Data.Repositories.Abstract
{
    public interface ITenantRepository : IRepository<TblAuthTenants, string>
    {
    }
}
