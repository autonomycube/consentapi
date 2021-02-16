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
            CreateMap<CreateTenantRequest, TblAuthTenants>();
            CreateMap<CreateTenantOnboardCommentRequest, TblAuthTenantOnboardStatus>();

            //response
            CreateMap<TblAuthTenants, TenantResponse>();
            CreateMap<TblAuthTenantOnboardStatus, TenantOnboardCommentResponse>();
        }
    }
}
