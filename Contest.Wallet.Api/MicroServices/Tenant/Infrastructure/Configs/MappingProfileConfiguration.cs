using AutoMapper;
using Consent.Api.Tenant.Services.DTO.Request;
using Consent.Api.Tenant.Services.DTO.Response;
using Consent.Common.EnityFramework.Entities;

namespace Consent.Api.Tenant.Infrastructure.Configs
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            // Mapping DTO to Models
            // request
            CreateMap<CreateTenantRequest, TblTenants>();
            CreateMap<CreateTenantOnboardCommentRequest, TblTenantOnboardStatus>();

            //response
            CreateMap<TblTenants, TenantResponse>();
            CreateMap<TblTenantOnboardStatus, TenantOnboardCommentResponse>();
        }
    }
}
