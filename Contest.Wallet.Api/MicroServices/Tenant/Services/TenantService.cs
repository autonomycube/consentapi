using AutoMapper;
using Consent.Api.Tenant.Data.DbContexts;
using Consent.Api.Tenant.Data.Repositories.Abstract;
using Consent.Api.Tenant.Services.Abstract;
using Consent.Api.Tenant.Services.DTO.Request;
using Consent.Api.Tenant.Services.DTO.Response;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Common.Data.UOW.Abstract;
using Consent.Common.EnityFramework.Entities;
using Consent.Common.EnityFramework.Entities.Identity;
using Consent.Common.Helpers.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consent.Api.Tenant.Services
{
    public class TenantService : ITenantService
    {
        #region Private Variables

        private readonly ITenantRepository _tenantRepository;
        private readonly ITenantOnboardStatusRepository _tenantOnboardStatusRepository;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<TenantService> _logger;
        private readonly IUnitOfWork<TenantDbContext> _unitOfWork;
        private readonly IBaseAuthHelper _baseAuthHelper;

        #endregion

        #region Constructor

        public TenantService(ITenantRepository tenantRepository,
            ITenantOnboardStatusRepository tenantOnboardStatusRepository,
            UserManager<UserIdentity> userManager,
            IMapper mapper,
            ILogger<TenantService> logger,
            IUnitOfWork<TenantDbContext> unitOfWork,
            IBaseAuthHelper baseAuthHelper)
        {
            _tenantRepository = tenantRepository
                ?? throw new ArgumentNullException(nameof(tenantRepository));
            _tenantOnboardStatusRepository = tenantOnboardStatusRepository
                ?? throw new ArgumentNullException(nameof(tenantOnboardStatusRepository));
            _userManager = userManager
                ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
            _baseAuthHelper = baseAuthHelper
                ?? throw new ArgumentNullException(nameof(baseAuthHelper));
        }

        #endregion

        #region CRUD - C

        public async Task<TenantResponse> CreateTenant(CreateTenantRequest request)
        {
            try
            {
                var entity = _mapper.Map<TblAuthTenants>(request);
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
                entity.IsActive = true;
                entity.CreatedBy = _baseAuthHelper.GetUserId();
                entity.UpdatedBy = _baseAuthHelper.GetUserId();

                _unitOfWork.BeginTransaction();
                var response = await _tenantRepository.Add(entity);
                var userIdentity = _mapper.Map<UserIdentity>(entity);
                userIdentity.UserName = "admin";
                userIdentity.IsActive = true;
                userIdentity.CreatedBy = _baseAuthHelper.GetUserId();
                userIdentity.UpdatedBy = _baseAuthHelper.GetUserId();
                userIdentity.TenantId = entity.Id;
                var result = await _userManager.CreateAsync(userIdentity);
                if (!result.Succeeded)
                {
                    throw new Exception($"{result.Errors.First().Description}");
                }
                await _unitOfWork.CommitAsync();

                return _mapper.Map<TenantResponse>(response);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw ex;
            }
        }

        public async Task<bool> CreateOnboardComment(CreateTenantOnboardCommentRequest request)
        {
            try
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

                _unitOfWork.BeginTransaction();
                var entity = _mapper.Map<TblAuthTenantOnboardStatus>(request);
                entity.CreatedBy = _baseAuthHelper.GetUserId();
                entity.UpdatedBy = _baseAuthHelper.GetUserId();
                await _tenantOnboardStatusRepository.Add(entity);

                tenant.TenantStatus = request.Status;
                tenant.UpdatedDate = DateTime.UtcNow;
                tenant.UpdatedBy = _baseAuthHelper.GetUserId();
                await _tenantRepository.Update(tenant);
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw ex;
            }
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
