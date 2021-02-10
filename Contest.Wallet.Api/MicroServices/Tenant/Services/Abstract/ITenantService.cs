using System.Threading.Tasks;
using Consent.Api.Tenant.Services.DTO.Request;
using Consent.Api.Tenant.Services.DTO.Response;

namespace Consent.Api.Tenant.Services.Abstract
{
    public interface ITenantService
    {
        Task<TenantResponse> CreateTenant(CreateTenantRequest request);
        Task<bool> UpdateTenant(UpdateTenantRequest request);
        Task<TenantResponse> Get(string id);
    }
}
