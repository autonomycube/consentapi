using AutoMapper;
using Consent.Api.Notification.DTO.Response;
using Consent.Api.Notification.Services.Abstract;
using Consent.Api.Tenant.Data.DbContexts;
using Consent.Api.Tenant.Data.Repositories.Abstract;
using Consent.Api.Tenant.DTO.Request;
using Consent.Api.Tenant.DTO.Response;
using Consent.Api.Tenant.Services.Abstract;
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
    public class HolderService : IHolderService
    {
        #region Private Variables

        private readonly ITenantRepository _tenantRepository;
        private readonly ITenantOnboardStatusRepository _tenantOnboardStatusRepository;
        private readonly IInvitationsRepository _invitationsRepository;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly ILogger<TenantService> _logger;
        private readonly IUnitOfWork<TenantDbContext> _unitOfWork;
        private readonly IBaseAuthHelper _baseAuthHelper;

        #endregion

        #region Constructor

        public HolderService(ITenantRepository tenantRepository,
            ITenantOnboardStatusRepository tenantOnboardStatusRepository,
            IInvitationsRepository invitationsRepository,
            UserManager<UserIdentity> userManager,
            INotificationService notificationService,
            IMapper mapper,
            ILogger<TenantService> logger,
            IUnitOfWork<TenantDbContext> unitOfWork,
            IBaseAuthHelper baseAuthHelper)
        {
            _tenantRepository = tenantRepository
                ?? throw new ArgumentNullException(nameof(tenantRepository));
            _tenantOnboardStatusRepository = tenantOnboardStatusRepository
                ?? throw new ArgumentNullException(nameof(tenantOnboardStatusRepository));
            _invitationsRepository = invitationsRepository
                ?? throw new ArgumentNullException(nameof(invitationsRepository));
            _userManager = userManager
                ?? throw new ArgumentNullException(nameof(userManager));
            _notificationService = notificationService
                ?? throw new ArgumentNullException(nameof(notificationService));
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

        #region Public Methods

        public IEnumerable<HolderEmailAddressesResponse> ValidateEmails(ValidateEmailAddressesRequest request)
        {
            return _invitationsRepository.ValidateEmails(request.Emails);
        }


        public async Task<IEnumerable<HolderEmailAddressesResponse>> SendEmailInvitations(HolderInviteEmailsRequest request)
        {
            try
            {
                var emailStatus = _invitationsRepository.ValidateEmails(request.Emails).ToList();
                var validEmails = emailStatus.Where(s => s.Status == EnumHolderEmailAddressStatus.Valid).Select(s => s.Email);
                if (validEmails.Count() > 0)
                {
                    _unitOfWork.BeginTransaction();
                    var response = await _notificationService.SendEmailInvitations(validEmails.ToList());
                    if (response.Success)
                    {
                        foreach (var status in emailStatus)
                        {
                            if (status.Status == EnumHolderEmailAddressStatus.Valid)
                            {
                                await _invitationsRepository.Add(new TblTenantInvitations
                                {
                                    Email = status.Email,
                                    Registered = false,
                                    TenantId = _baseAuthHelper.GetTenantId(),
                                    CreatedBy = _baseAuthHelper.GetUserId(),
                                    CreatedDate = DateTime.UtcNow,
                                    UpdatedBy = _baseAuthHelper.GetUserId(),
                                    UpdatedDate = DateTime.UtcNow
                                });

                                status.Status = EnumHolderEmailAddressStatus.InvitationSent;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(response.Message);
                    }
                }
                await _unitOfWork.CommitAsync();

                return emailStatus;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw ex;
            }
        }

        #endregion
    }
}
