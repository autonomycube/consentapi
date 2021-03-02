using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Consent.Api.Notification.DTO.Response;
using Consent.Api.Tenant.DTO.Request;
using Consent.Api.Tenant.DTO.Response;
using Consent.Api.Tenant.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Consent.Api.Tenant.API.v1
{
    [Produces("application/json")]
    [Route("api/v1/holders")]
    [ApiController]
    public class HoldersController : ControllerBase
    {
        #region Private Variables

        private readonly IHolderService _holderService;
        private readonly ILogger<HoldersController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public HoldersController(IHolderService holderService,
            IMapper mapper,
            ILogger<HoldersController> logger)
        {
            _holderService = holderService
                ?? throw new ArgumentNullException(nameof(holderService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - C

        /// <summary>
        /// Validates Email Addresses
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /holders/validateemails
        ///     {
        ///         "emails": [
        ///             "lovaraju.sappidi@gmail.com",
        ///             "lsappidi@sweyainfotech.com"
        ///         ]
        ///     }
        ///     
        /// Sample Response:
        /// 
        ///     POST /holders/validateemails
        ///     {
        ///         "message": "Date fetched successfully",
        ///         "result": [
        ///             {
        ///                 "email": "lovaraju.sappidi@gmail.com",
        ///                 "status": 1
        ///             },
        ///             {
        ///                 "email": "lsappidi@sweyainfotech.com",
        ///                 "status": 0
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <param name="request">Validate Email Addresses Request</param>
        /// <returns>Returns Validate Email Addresses Response</returns>
        /// <response code="200">Returns Validate Email Addresses Response</response>
        [HttpPost("validateemails")]
        [ProducesResponseType(typeof(IEnumerable<HolderEmailAddressesResponse>), Status200OK)]
        public ApiResponse CreateTenant([FromBody] ValidateEmailAddressesRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }

            try
            {
                var result = _holderService.ValidateEmails(request);
                return new ApiResponse("Date fetched successfully", result, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Invites Email addresses
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /holder/invites
        ///     {
        ///         "emails": [
        ///             "xxxx@gmail.com"
        ///         ]
        ///     }
        ///     
        /// Sample response:
        ///
        ///     POST /holder/invites
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
        /// <param name="dto">Holder Invite Emails Request</param>
        /// <returns>Returns Emails success model</returns>
        /// <response code="200">Returns Emails success model</response>
        [HttpPost("invites")]
        [ProducesResponseType(typeof(IEnumerable<HolderEmailAddressesResponse>), Status200OK)]
        public async Task<ApiResponse> SendEmailInvitations([FromBody] HolderInviteEmailsRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(Status400BadRequest, ModelState.AllErrors());
            }

            try
            {
                var response = await _holderService.SendEmailInvitations(dto);
                return new ApiResponse("Email sent successfully.", response);
            }
            catch (Exception ex)
            {
                _logger.LogError("SendEmailInvitations - Exception: " + ex.Message);
                throw new ApiException(ex);
            }
        }


        #endregion
    }
}