using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Consent.Api.Auth.DTO.Request;
using Consent.Api.Auth.Services.Abstract;
using Consent.Api.Notification.DTO.Request;
using Consent.Api.Notification.DTO.Response;
using Consent.Api.Notification.Services.Abstract;
using Consent.Common.EnityFramework.Entities.Identity;
using Consent.Common.Helpers.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Consent.Api.Auth.API.v1
{
    [Produces("application/json")]
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Private Variables

        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly IBaseAuthHelper _baseAuthHelper;

        #endregion

        #region Constructor

        public AuthController(INotificationService notificationService,
            IUserService userService,
            UserManager<UserIdentity> userManager,
            IMapper mapper,
            ILogger<AuthController> logger,
            IBaseAuthHelper baseAuthHelper)
        {
            _notificationService = notificationService
                ?? throw new ArgumentNullException(nameof(notificationService));
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

        #region User

        /// <summary>
        /// Generates PhoneNumber Confirmation Otp
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/user/generateotp
        ///     {
        ///        "phoneNumber": "+91xxxxxxxx"
        ///     }
        ///     
        /// Sample response:
        ///
        ///     POST /auth/user/generateotp
        ///     {
        ///         "message": "Otp generated successfully.",
        ///         "isError": false,
        ///         "result": {
        ///             "success": true,
        ///             "message": "Sms sent successfully.",
        ///             "referenceId": "xxxx-xxxx-xxxx-xxxxxxxx"
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Mobile number Request</param>
        /// <returns>Returns Mobile number success model</returns>
        /// <response code="200">Returns Mobile number success model</response>
        [HttpPost("user/generateotp")]
        [ProducesResponseType(typeof(GenerateOtpResponse), Status200OK)]
        public async Task<ApiResponse> GeneratePhoneNumberConfirmationOtp([FromBody] GenerateOtpRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(Status400BadRequest, ModelState.AllErrors());
            }

            try
            {
                var user = await _userService.GetByPhoneNumber(request.PhoneNumber);
                if (user == null)
                {
                    throw new ApiException($"PhoneNumber {request.PhoneNumber} is not registered.");
                }

                if (user.IsActive != null && !user.IsActive.Value)
                {
                    throw new ApiException("User is not activated.");
                }

                if (user.PhoneNumberConfirmed)
                {
                    throw new ApiException($"PhoneNumber {user.PhoneNumber} already confirmed.");
                }

                var response = await _notificationService.SendConfirmationOtp(user);
                return new ApiResponse("Otp generated successfully.", response);
            }
            catch (Exception ex)
            {
                _logger.LogError("GeneratePhoneNumberConfirmationOtp - Exception: " + ex.Message);
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Verifies PhoneNumber Confirmation Otp
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/user/verifyotp
        ///     {
        ///        "phoneNumber": "+91xxxxxxxx",
        ///        "otp": "xxxx",
        ///        "referenceId": "xxxx-xxxx-xxxx-xxxxxxxx"
        ///     }
        ///     
        /// Sample response:
        ///
        ///     POST /auth/user/verifyotp
        ///     {
        ///         "message": "Otp verified successfully.",
        ///         "isError": false,
        ///         "result": {
        ///             "success": true,
        ///             "message": "Otp verified successfully."
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <param name="request">OTP Request</param>
        /// <returns>Returns OTP success model</returns>
        /// <response code="200">Returns OTP success model</response>
        [HttpPost("user/verifyotp")]
        [ProducesResponseType(typeof(SuccessResponse), Status200OK)]
        public async Task<ApiResponse> VerifyPhoneNumberConfirmationOtp([FromBody] VerifyOtpRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(Status400BadRequest, ModelState.AllErrors());
            }

            try
            {
                var user = await _userService.GetByPhoneNumber(request.PhoneNumber);
                if (user == null)
                {
                    throw new ApiException($"PhoneNumber {request.PhoneNumber} is not registered.");
                }

                if (user.IsActive != null && !user.IsActive.Value)
                {
                    throw new ApiException("User is not activated.");
                }

                if (user.PhoneNumberConfirmed)
                {
                    throw new ApiException($"PhoneNumber {user.PhoneNumber} already confirmed.");
                }

                var response = await _notificationService.VerifyConfirmationOtp(request, user);
                if (response.Success)
                {
                    user.PhoneNumberConfirmed = true;
                    user.UpdatedBy = _baseAuthHelper.GetUserId();
                    user.UpdatedDate = DateTime.UtcNow;
                    await _userService.Update(user);
                }

                return new ApiResponse("Otp verified successfully.", response);
            }
            catch (Exception ex)
            {
                _logger.LogError("VerifyPhoneNumberConfirmationOtp - Exception: " + ex.Message);
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Generates Email Confirmation Link
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/user/confirmemail
        ///     {
        ///        "email": "xxxx@xxxx.com"
        ///     }
        ///     
        /// Sample response:
        ///
        ///     POST /auth/user/confirmemail
        ///     {
        ///         "message": "Link generated successfully.",
        ///         "isError": false,
        ///         "result": {
        ///             "success": true,
        ///             "message": "Email sent successfully."
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Email Confirmation Request</param>
        /// <returns>Returns Mobile number success model</returns>
        /// <response code="200">Returns Mobile number success model</response>
        [HttpPost("user/confirmemail")]
        [ProducesResponseType(typeof(SuccessResponse), Status200OK)]
        public async Task<ApiResponse> GenerateEmailConfirmationLink([FromBody] EmailConfirmationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(Status400BadRequest, ModelState.AllErrors());
            }

            try
            {
                var user = await _userService.GetByEmail(request.Email);
                if (user == null)
                {
                    throw new ApiException($"Email {request.Email} is not registered.", Status404NotFound);
                }

                if (user.IsActive != null && !user.IsActive.Value)
                {
                    throw new ApiException("User is not activated.");
                }

                if (user.EmailConfirmed)
                {
                    throw new ApiException($"Email already confirmed.");
                }

                string emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("account/confirmemail", "auth", new { userId = user.Id, emailConfirmationToken }, HttpContext.Request.Scheme);
                var response = await _notificationService.SendEmailConfirmLink(request.Email, callbackUrl);
                return new ApiResponse("Link generated successfully.", response);
            }
            catch (Exception ex)
            {
                _logger.LogError("GenerateEmailConfirmationLink - Exception: " + ex.Message);
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Invites Email addresses
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/invite
        ///     {
        ///         "emailList": [
        ///             "xxxx@gmail.com"
        ///         ]
        ///     }
        ///     
        /// Sample response:
        ///
        ///     POST /auth/invite
        ///     {
        ///         "message": "Email sent successfully.",
        ///         "isError": false,
        ///         "result": {
        ///             "success": true,
        ///             "message": "Email sent successfully."
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <param name="dto">Mobile number Request</param>
        /// <returns>Returns Mobile number success model</returns>
        /// <response code="200">Returns Mobile number success model</response>
        [HttpPost("invites")]
        [ProducesResponseType(typeof(SuccessResponse), Status200OK)]
        public async Task<ApiResponse> SendEmailInvitations([FromBody] InviteEmailsRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(Status400BadRequest, ModelState.AllErrors());
            }

            try
            {
                var response = await _notificationService.SendEmailInvitations(dto.EmailList);
                return new ApiResponse("Email sent successfully.", response);
            }
            catch (Exception ex)
            {
                _logger.LogError("SendEmailInvitations - Exception: " + ex.Message);
                throw new ApiException(ex);
            }
        }

        #endregion

        #region Account

        /// <summary>
        /// Confirms Email
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/account/confirmemail?userId=xxxx-xxxx-xxxx-xxxxxxxx{seperator}code=xxx
        ///     
        /// Sample response:
        ///
        ///     POST /auth/account/confirmemail?userId=xxxx-xxxx-xxxx-xxxxxxxx{seperator}code=xxx
        ///     {
        ///         "message": "Email confirmed successfully.",
        ///         "isError": false,
        ///         "result": true
        ///     }
        ///
        /// </remarks>
        /// <param name="userId">User Id</param>
        /// <param name="code">Token</param>
        /// <returns>Returns true/false</returns>
        /// <response code="200">Returns true/false</response>
        [HttpGet("account/confirmemail")]
        [ProducesResponseType(typeof(bool), Status200OK)]
        public async Task<ApiResponse> ConfirmEmail([FromQuery]string userId, [FromQuery]string code)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(Status400BadRequest, ModelState.AllErrors());
            }

            try
            {
                var user = _userService.GetById(userId);
                if (user == null)
                {
                    throw new ApiException($"UserId {userId} is not found.");
                }

                if (user.IsActive != null && !user.IsActive.Value)
                {
                    throw new ApiException("User is not activated.");
                }

                if (user.EmailConfirmed)
                {
                    throw new Exception($"Email {user.Email} already confirmed.");
                }

                user.EmailConfirmed = true;
                user.UpdatedBy = _baseAuthHelper.GetUserId();
                user.UpdatedDate = DateTime.UtcNow;
                await _userService.Update(user);
                return new ApiResponse("Email confirmed successfully.", true);
            }
            catch (Exception ex)
            {
                _logger.LogError("GeneratePhoneNumberConfirmationOtp - Exception: " + ex.Message);
                throw new ApiException(ex);
            }
        }

        #endregion
    }
}