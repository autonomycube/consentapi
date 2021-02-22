using System.Collections.Generic;
using System.Threading.Tasks;
using Consent.Api.Tenant.Services.DTO.Request;
using Consent.Api.Tenant.Services.DTO.Response;
using Consent.Common.Data;
using Consent.Common.Repository.Helpers;

namespace Consent.Api.Tenant.Services.Abstract
{
    public interface ITenantService
    {
        Task<TenantResponse> CreateTenant(CreateTenantRequest request);
        Task<bool> CreateOnboardComment(CreateTenantOnboardCommentRequest request);
        Task<IEnumerable<TenantOnboardCommentResponse>> GetTenantOnboardComments(string id);
        Task<bool> UpdateTenant(UpdateTenantRequest request);
        Task<TenantResponse> Get(string id);
        Task<PaginatedList<TenantResponse>> GetAll(int pageIndex, int pageSize, bool includeCount = false);
    }
}
