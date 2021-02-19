using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Consent.Api.Auth.DTO.Request;
using Consent.Api.Auth.DTO.Response;
using Consent.Api.Auth.Services.Abstract;
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

        private readonly IOtpService _otpService;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AuthController(IOtpService otpService,
            IMapper mapper,
            ILogger<AuthController> logger)
        {
            _otpService = otpService
                ?? throw new ArgumentNullException(nameof(otpService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - C

        /// <summary>
        /// Generates PhoneNumber Confirmation Otp
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/phonenumber/generateotp
        ///     {
        ///        "userId": "xxxx-xxxx-xxxx-xxxxxxxx"
        ///     }
        ///     
        /// Sample response:
        ///
        ///     POST /auth/phonenumber/generateotp
        ///     {
        ///         "message": "Otp generated successfully.",
        ///         "result": {
        ///             "success": true,
        ///             "message": "Sms sent successfully.",
        ///             "referenceId": "xxxx-xxxx-xxxx-xxxxxxxx"
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <param name="dto">Mobile number Request</param>
        /// <returns>Returns Mobile number success model</returns>
        /// <response code="200">Returns Mobile number success model</response>
        [HttpPost("phonenumber/generateotp")]
        [ProducesResponseType(typeof(GenerateOtpResponse), Status200OK)]
        public async Task<ApiResponse> GeneratePhoneNumberConfirmationOtp([FromBody] GenerateOtpRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(Status400BadRequest, ModelState.AllErrors());
            }

            try
            {
                var response = await _otpService.GeneratePhoneNumberConfirmationOtp(dto);
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
        ///     POST /auth/phonenumber/verifyotp
        ///     {
        ///        "userId": "xxxx-xxxx-xxxx-xxxxxxxx"
        ///     }
        ///     
        /// Sample response:
        ///
        ///     POST /auth/phonenumber/verifyotp
        ///     {
        ///         "message": "Otp verified successfully.",
        ///         "result": {
        ///             "success": true,
        ///             "message": "Otp verified successfully."
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <param name="dto">OTP Request</param>
        /// <returns>Returns OTP success model</returns>
        /// <response code="200">Returns OTP success model</response>
        [HttpPost("phonenumber/verifyotp")]
        [ProducesResponseType(typeof(VerifyOtpResponse), Status200OK)]
        public async Task<ApiResponse> VerifyPhoneNumberConfirmationOtp([FromBody] VerifyOtpRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(Status400BadRequest, ModelState.AllErrors());
            }

            try
            {
                var response = await _otpService.VerifyPhoneNumberConfirmationOtp(dto);
                return new ApiResponse("Otp verified successfully.", response);
            }
            catch (Exception ex)
            {
                _logger.LogError("VerifyPhoneNumberConfirmationOtp - Exception: " + ex.Message);
                throw new ApiException(ex);
            }
        }

        #endregion
    }
}