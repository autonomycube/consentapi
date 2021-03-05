using Consent.Common.Repository.SQL.Abstract;
using Consent.Common.EnityFramework.Entities;
using Consent.Api.Tenant.DTO.Response;

namespace Consent.Api.Tenant.Data.Repositories.Abstract
{
    public interface ITenantRepository : IRepository<TblAuthTenants, string>
    {
        HoldersStatusResponse GetHoldersStatusCount(string tenantId);
    }
}
