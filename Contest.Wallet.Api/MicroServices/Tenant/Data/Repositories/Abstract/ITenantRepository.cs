using Consent.Common.Repository.SQL.Abstract;
using Consent.Api.Tenant.Data.Entities;

namespace Consent.Api.Tenant.Data.Repositories.Abstract
{
    public interface ITenantRepository : IRepository<TblTenants, string>
    {
    }
}
