using AutoMapper;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Api.Tenant.Data.Repositories.Abstract;
using Consent.Api.Tenant.Services.Abstract;
using System;
using System.Threading.Tasks;
using Consent.Api.Tenant.Services.DTO.Response;
using Consent.Api.Tenant.Services.DTO.Request;

namespace Consent.Api.Tenant.Services
{
    public class TenantService : ITenantService
    {
        #region Private Variables

        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TenantService> _logger;

        #endregion

        #region Constructor

        public TenantService(ITenantRepository tenantRepository,
            IMapper mapper,
            ILogger<TenantService> logger)
        {
            _tenantRepository = tenantRepository
                ?? throw new ArgumentNullException(nameof(tenantRepository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - C

        public async Task<TenantResponse> CreateTenant(CreateTenantRequest request)
        {
            return await Task.FromResult(new TenantResponse 
            { 
            });
        }

        #endregion

        #region CRUD - R

        public async Task<TenantResponse> Get(string id)
        {
            return await Task.FromResult(new TenantResponse
            {
            });
        }

        #endregion

        #region CRUD - R

        public async Task<bool> UpdateTenant(UpdateTenantRequest request)
        {
            return await Task.FromResult(true);
        }

        #endregion
    }
}
