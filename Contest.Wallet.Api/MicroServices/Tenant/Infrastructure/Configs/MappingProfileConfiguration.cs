using AutoMapper;
using Consent.Api.Tenant.DTO.Request;
using Consent.Api.Tenant.DTO.Response;
using Consent.Common.EnityFramework.Entities;
using Consent.Common.EnityFramework.Entities.Identity;

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
            CreateMap<CreateHolderRequest, UserIdentity>();
            
            // response
            CreateMap<TblAuthTenants, TenantResponse>();
            CreateMap<TblAuthTenantOnboardStatus, TenantOnboardCommentResponse>();
            CreateMap<UserIdentity, HolderResponse>();

            // others
            CreateMap<TblAuthTenants, UserIdentity>();
        }
    }
}
