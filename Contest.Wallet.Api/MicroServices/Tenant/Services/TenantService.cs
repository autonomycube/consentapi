using AutoMapper;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Api.Tenant.Data.Repositories.Abstract;
using Consent.Api.Tenant.Services.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;
using Consent.Api.Tenant.Services.DTO.Response;
using Consent.Api.Tenant.Services.DTO.Request;
using Consent.Common.EnityFramework.Entities;
using System.Collections.Generic;

namespace Consent.Api.Tenant.Services
{
    public class TenantService : ITenantService
    {
        #region Private Variables

        private readonly ITenantRepository _tenantRepository;
        private readonly ITenantOnboardStatusRepository _tenantOnboardStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TenantService> _logger;

        #endregion

        #region Constructor

        public TenantService(ITenantRepository tenantRepository,
            ITenantOnboardStatusRepository tenantOnboardStatusRepository,
            IMapper mapper,
            ILogger<TenantService> logger)
        {
            _tenantRepository = tenantRepository
                ?? throw new ArgumentNullException(nameof(tenantRepository));
            _tenantOnboardStatusRepository = tenantOnboardStatusRepository
                ?? throw new ArgumentNullException(nameof(tenantOnboardStatusRepository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - C

        public async Task<TenantResponse> CreateTenant(CreateTenantRequest request)
        {
            var entity = _mapper.Map<TblTenants>(request);
            if (entity.EmployeesCount <= 20)
            {
                entity.TenantType = TenantType.Startup;
            }
            else if (entity.EmployeesCount <= 50)
            {
                entity.TenantType = TenantType.BGV;
            }
            else
            {
                entity.TenantType = TenantType.RA;
            }
            var response = await _tenantRepository.Add(entity);
            return _mapper.Map<TenantResponse>(response);
        }

        public async Task<bool> CreateOnboardComment(CreateTenantOnboardCommentRequest request)
        {
            if (request.Status == TenantStatus.Registered)
            {
                throw new Exception($"Request Status is invalid.");
            }

            var tenant = await _tenantRepository.GetById(request.TenantId);
            if (tenant == null)
            {
                throw new KeyNotFoundException($"Tenant {request.TenantId} is not found.");
            }

            if (tenant.TenantStatus == TenantStatus.OnboardComplete || tenant.TenantStatus == TenantStatus.OnboardRejected)
            {
                throw new Exception($"Onboard Comments can't be created on Tenant {request.TenantId}.");
            }

            var entity = _mapper.Map<TblTenantOnboardStatus>(request);
            await _tenantOnboardStatusRepository.Add(entity);

            tenant.TenantStatus = request.Status;
            tenant.UpdatedDate = DateTime.UtcNow;
            await _tenantRepository.Update(tenant);
            return true;
        }

        #endregion

        #region CRUD - R

        public async Task<TenantResponse> Get(string id)
        {
            var result = await _tenantRepository.GetById(id);
            return _mapper.Map<TenantResponse>(result);
        }

        public async Task<IEnumerable<TenantOnboardCommentResponse>> GetTenantOnboardComments(string id)
        {
            var result = await _tenantOnboardStatusRepository.FindBy(t => t.TenantId == id);
            return _mapper.Map<IEnumerable<TenantOnboardCommentResponse>>(result.ToList());
        }

        #endregion

        #region CRUD - U

        public async Task<bool> UpdateTenant(UpdateTenantRequest request)
        {
            return await Task.FromResult(true);
        }

        #endregion
    }
}
