using Consent.Api.Notification.Data.Repositories.Abstract;
using Consent.Api.Notification.DTO.Request;
using Consent.Api.Notification.DTO.Response;
using Consent.Api.Notification.Services.Abstract;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Common.Constants;
using Consent.Common.EnityFramework.Entities;
using Consent.Common.EnityFramework.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consent.Api.Notification.Services
{
    public class NotificationService : INotificationService
    {
        #region Private Variables

        private readonly IOtpTrackerRepository _otpTrackerRepository;
        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;
        private readonly ILogger<NotificationService> _logger;

        #endregion

        #region Constructor

        public NotificationService(IOtpTrackerRepository otpTrackerRepository,
            ISmsService smsService,
            IEmailService emailService,
            ILogger<NotificationService> logger)
        {
            _otpTrackerRepository = otpTrackerRepository
                ?? throw new ArgumentNullException(nameof(otpTrackerRepository));
            _smsService = smsService
                ?? throw new ArgumentNullException(nameof(smsService));
            _emailService = emailService
                ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Public Methods

        public async Task<GenerateOtpResponse> SendConfirmationOtp(UserIdentity user)
        {
            try
            {
                var smsRequest = new CreateTmpSmsRequest()
                {
                    MobileNumber = user.PhoneNumber,
                    Context = NotificationConsts.RegistrationContext,
                    SubContext = NotificationConsts.PhoneNumberConfirmationSubContext,
                    ContextId = user.Id,
                    IsArabic = false
                };
                var result = await _smsService.SendSingleTemplateSMS(smsRequest);
                if (!string.IsNullOrEmpty(result.ErrMessage))
                {
                    throw new Exception(result.ErrMessage);
                }

                return new GenerateOtpResponse
                {
                    Success = true,
                    Message = "Sms successfully sent.",
                    ReferenceId = result.OutputMessage
                };
            }
            catch (Exception ex)
            {
                return new GenerateOtpResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<SuccessResponse> VerifyConfirmationOtp(VerifyOtpRequest request, UserIdentity user)
        {
            try
            {
                TblNotifyOtpTracker otpDetails = await _otpTrackerRepository.GetById(request.ReferenceId);
                if (otpDetails == null)
                {
                    throw new Exception($"ReferenceId {request.ReferenceId} is not valid.");
                }

                if (!otpDetails.Otp.Trim().Equals(request.Otp.Trim()))
                {
                    throw new Exception($"Otp {request.Otp} is not valid.");
                }

                otpDetails.OtpVerified = true;
                await _otpTrackerRepository.Update(otpDetails);
                return new SuccessResponse
                {
                    Success = true,
                    Message = "OTP verified successfully."
                };
            }
            catch (Exception ex)
            {
                return new SuccessResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<SuccessResponse> SendEmailConfirmLink(string email, string link)
        {
            try
            {
                var emailRequest = new CreateTmpEmailRequest()
                {
                    Context = NotificationConsts.RegistrationContext,
                    SubContext = NotificationConsts.EmailConfirmationSubContext,
                    Email = email,
                    PlaceHolders = new Dictionary<string, string>()
                    {
                        { "CompanyName", "Consent" },
                        { "Link", link }
                    },
                    IsArabic = false
                };
                var result = await _emailService.SendSingleTemplateEmail(emailRequest);
                if (!string.IsNullOrEmpty(result.ErrMessage))
                {
                    throw new Exception(result.ErrMessage);
                }

                return new SuccessResponse
                {
                    Success = true,
                    Message = "Email sent successfully."
                };
            }
            catch (Exception ex)
            {
                return new SuccessResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<SuccessResponse> SendRegistrationEmail(UserIdentity user)
        {
            try
            {
                var emailRequest = new CreateTmpEmailRequest()
                {
                    Context = NotificationConsts.TenantOnboard,
                    SubContext = NotificationConsts.TenantOnboardRegistered,
                    Email = user.Email,
                    PlaceHolders = new Dictionary<string, string>()
                    {
                        { "UserName", user.FirstName }
                    },
                    IsArabic = false
                };
                var result = await _emailService.SendSingleTemplateEmail(emailRequest);
                if (!string.IsNullOrEmpty(result.ErrMessage))
                {
                    throw new Exception(result.ErrMessage);
                }

                return new SuccessResponse
                {
                    Success = true,
                    Message = "Email sent successfully."
                };
            }
            catch (Exception ex)
            {
                return new SuccessResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<SuccessResponse> SendTenantOnboardCompletedEmail(string email)
        {
            try
            {
                var emailRequest = new CreateTmpEmailRequest()
                {
                    Context = NotificationConsts.TenantOnboard,
                    SubContext = NotificationConsts.TenantOnboardCompleted,
                    Email = email,
                    PlaceHolders = new Dictionary<string, string>()
                    {
                    },
                    IsArabic = false
                };
                var result = await _emailService.SendSingleTemplateEmail(emailRequest);
                if (!string.IsNullOrEmpty(result.ErrMessage))
                {
                    throw new Exception(result.ErrMessage);
                }

                return new SuccessResponse
                {
                    Success = true,
                    Message = "Email sent successfully."
                };
            }
            catch (Exception ex)
            {
                return new SuccessResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<SuccessResponse> SendTenantOnboardRejectedEmail(string email)
        {
            try
            {
                var emailRequest = new CreateTmpEmailRequest()
                {
                    Context = NotificationConsts.TenantOnboard,
                    SubContext = NotificationConsts.TenantOnboardRejected,
                    Email = email,
                    PlaceHolders = new Dictionary<string, string>()
                    {
                    },
                    IsArabic = false
                };
                var result = await _emailService.SendSingleTemplateEmail(emailRequest);
                if (!string.IsNullOrEmpty(result.ErrMessage))
                {
                    throw new Exception(result.ErrMessage);
                }

                return new SuccessResponse
                {
                    Success = true,
                    Message = "Email sent successfully."
                };
            }
            catch (Exception ex)
            {
                return new SuccessResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<SuccessResponse> SendEmailInvitations(List<string> emails)
        {
            try
            {
                var emailRequest = new MultipleTmpEmailRequest()
                {
                    Context = NotificationConsts.InviteEmailsContext,
                    SubContext = NotificationConsts.InviteEmailsSubContext,
                    EmailList = emails,
                    PlaceHolders = new Dictionary<string, string>()
                    {
                        { "CompanyName", "Consent" }
                    },
                    IsArabic = false
                };
                var result = await _emailService.SendMultipleTemplatedEmail(emailRequest);
                if (!string.IsNullOrEmpty(result.ErrMessage))
                {
                    throw new Exception(result.ErrMessage);
                }

                return new SuccessResponse
                {
                    Success = true,
                    Message = "Email sent successfully."
                };
            }
            catch (Exception ex)
            {
                return new SuccessResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        #endregion
    }
}
