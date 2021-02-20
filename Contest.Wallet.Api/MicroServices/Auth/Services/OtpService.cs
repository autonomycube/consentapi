using AutoMapper;
using Consent.Api.Auth.DTO.Request;
using Consent.Api.Auth.DTO.Response;
using Consent.Api.Auth.Services.Abstract;
using Consent.Api.Notification.Data.Repositories.Abstract;
using Consent.Api.Notification.DTO.Request;
using Consent.Api.Notification.Services.Abstract;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Common.Constants;
using Consent.Common.EnityFramework.Entities;
using Consent.Common.EnityFramework.Entities.Identity;
using Consent.Common.Helpers.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consent.Api.Auth.Services
{
    public class OtpService : IOtpService
    {
        #region Private Variables

        private readonly IOtpTrackerRepository _otpTrackerRepository;
        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<OtpService> _logger;
        private readonly IBaseAuthHelper _baseAuthHelper;

        #endregion

        #region Constructor

        public OtpService(IOtpTrackerRepository otpTrackerRepository,
            ISmsService smsService,
            IEmailService emailService,
            IUserService userService,
            UserManager<UserIdentity> userManager,
            IMapper mapper,
            ILogger<OtpService> logger,
            IBaseAuthHelper baseAuthHelper)
        {
            _otpTrackerRepository = otpTrackerRepository
                ?? throw new ArgumentNullException(nameof(otpTrackerRepository));
            _smsService = smsService
                ?? throw new ArgumentNullException(nameof(smsService));
            _emailService = emailService
                ?? throw new ArgumentNullException(nameof(emailService));
            _userService = userService
                ?? throw new ArgumentNullException(nameof(userService));
            _userManager = userManager
                ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
            _baseAuthHelper = baseAuthHelper
                ?? throw new ArgumentNullException(nameof(baseAuthHelper));
        }

        #endregion

        #region Public Methods

        public async Task<GenerateOtpResponse> GeneratePhoneNumberConfirmationOtp(GenerateOtpRequest request)
        {
            try
            {
                var user = _userService.GetById(request.UserId);
                if (user == null)
                {
                    throw new KeyNotFoundException($"UserId {request.UserId} is not found.");
                }

                if (user.PhoneNumberConfirmed)
                {
                    throw new Exception($"PhoneNumber {user.PhoneNumber} already confirmed.");
                }

                var smsRequest = new CreateTmpSmsRequest()
                {
                    MobileNumber = user.PhoneNumber,
                    Context = NotificationConsts.PhoneNumberConfirmationContext,
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
            catch(Exception ex)
            {
                return new GenerateOtpResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<VerifyOtpResponse> VerifyPhoneNumberConfirmationOtp(DTO.Request.VerifyOtpRequest request)
        {
            try
            {
                var user = _userService.GetById(request.UserId);
                if (user == null)
                {
                    throw new KeyNotFoundException($"UserId {request.UserId} is not found.");
                }

                if (user.PhoneNumberConfirmed)
                {
                    throw new Exception($"PhoneNumber {user.PhoneNumber} already confirmed.");
                }

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
                user.PhoneNumberConfirmed = true;
                user.UpdatedBy = _baseAuthHelper.GetUserId();
                user.UpdatedDate = DateTime.UtcNow;
                await _userService.Update(user);
                return new VerifyOtpResponse
                {
                    Success = true,
                    Message = "OTP verified successfully."
                };
            }
            catch (Exception ex)
            {
                return new VerifyOtpResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<GenerateEmailConfirmationLinkResponse> GenerateEmailConfirmationLink(GenerateEmailConfirmationLinkRequest request)
        {
            try
            {
                var user = _userService.GetById(request.UserId);
                if (user == null)
                {
                    throw new KeyNotFoundException($"UserId {request.UserId} is not found.");
                }

                if (user.EmailConfirmed)
                {
                    throw new Exception($"Email already confirmed.");
                }

                string emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var emailRequest = new CustomEmailRequest
                {
                    emailAddress = user.Email,
                    htmlText = emailConfirmationToken,
                    mailSubject = "Consent Email Confirmation"
                };
                var result = await _emailService.SendCustomEmail(emailRequest);
                if (result)
                {
                    return new GenerateEmailConfirmationLinkResponse
                    {
                        Success = false,
                        Message = "Email sent successfully."
                    };
                }
                else
                {
                    return new GenerateEmailConfirmationLinkResponse
                    {
                        Success = true,
                        Message = "Email sent successfully."
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenerateEmailConfirmationLinkResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<InviteEmailsResponse> SendEmailInvitations(InviteEmailsRequest request)
        {
            try
            {
                var emailRequest = new MultipleTmpEmailRequest()
                {
                    Context = NotificationConsts.InviteEmailsContext,
                    SubContext = NotificationConsts.InviteEmailsSubContext,
                    EmailList = request.EmailList,
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

                return new InviteEmailsResponse
                {
                    Success = true,
                    Message = "Email sent successfully."
                };
            }
            catch (Exception ex)
            {
                return new InviteEmailsResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        #endregion
    }
}
