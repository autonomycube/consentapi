using Consent.Api.Tenant.DTO.Request;
using Consent.Api.Tenant.DTO.Response;
using Consent.Common.Repository.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consent.Api.Tenant.Services.Abstract
{
    public interface ITenantService
    {
        Task<TenantResponse> CreateTenant(CreateTenantRequest request);
        Task<bool> CreateOnboardComment(CreateTenantOnboardCommentRequest request);
        Task<IEnumerable<TenantOnboardCommentResponse>> GetTenantOnboardComments(string id);
        Task<bool> UpdateTenant(UpdateTenantRequest request);
        Task<TenantResponse> Get(string id);
        Task<TenantDashboardResponse> GetTenantDashboard();
        Task<PaginatedList<TenantResponse>> GetOnboardPendingTenantsPages(int pageIndex, int pageSize, TenantFilter filters, bool includeCount = false);
    }
}
