using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Consent.Api.Notification.API.v1.DTO.Request;
using Consent.Api.Notification.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Consent.Api.Notification.API.v1
{
    [Produces("application/json")]
    [Route("api/v1/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        #region Private Variables

        private readonly ISMSService _smsService;
        private readonly IEmailService _emailService;
        private readonly ILogger<NotificationController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public NotificationController(ISMSService smsService,
            IEmailService emailService,
            IMapper mapper,
            ILogger<NotificationController> logger)
        {
            _smsService = smsService
                ?? throw new ArgumentNullException(nameof(smsService));
            _emailService = emailService
                ?? throw new ArgumentNullException(nameof(emailService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - C

        /// <summary>
        /// Send OTP to Mobilenumber
        /// </summary>
        /// <param name="request">sms otp request parameters</param>
        /// <returns></returns>
        [HttpPost("sms/sendotp")]
        public async Task<ApiResponse> SendOTP([FromBody] CreateSmsOtpRequest request)
        {
            if (ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }

            try
            {
                var result = await _smsService.SendOTP(request);
                return new ApiResponse("Otp sent successfully.", result, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Verifies OTP
        /// </summary>
        /// <param name="request">verify otp request parameters</param>
        /// <returns></returns>
        [HttpPost("sms/verifyotp")]
        public async Task<ApiResponse> VerifyOTP([FromBody] VerifySmsOtpRequest request)
        {
            if (ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }

            try
            {
                var result = await _smsService.VerifyOTP(request);
                return new ApiResponse("Otp verified successfully.", result, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="request">sms otp request parameters</param>
        /// <returns></returns>
        [HttpPost("email/send")]
        public async Task<ApiResponse> SendEmail([FromBody] CreateEmailRequest request)
        {
            if (ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }

            try
            {
                var result = await _emailService.SendEmail(request);
                return new ApiResponse("Email sent successfully.", result, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        #endregion
    }
}