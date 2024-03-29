﻿using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Consent.Api.Tenant.DTO.Request;
using Consent.Api.Tenant.DTO.Response;
using Consent.Api.Tenant.Services.Abstract;
using Consent.Common.Configuration.Options.HelperModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Consent.Api.Tenant.API.v1
{
    [Produces("application/json")]
    [Route("api/v1/tenant")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        #region Private Variables

        private readonly ITenantService _tenantService;
        private readonly ILogger<TenantController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public TenantController(ITenantService tenantService,
            IMapper mapper,
            ILogger<TenantController> logger)
        {
            _tenantService = tenantService
                ?? throw new ArgumentNullException(nameof(tenantService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - C

        /// <summary>
        /// Creates Tenant
        /// </summary>
        /// <remarks>
        /// Sample Response:
        /// 
        ///     POST /tenant
        ///     {
        ///         "message": "Tenant created successfully",
        ///         "result": {
        ///             "id": "9c441515-0820-4620-9dd6-97bcb1727248",
        ///             "contact": "string",
        ///             "address": "string",
        ///             "email": "string",
        ///             "employeesCount": 0,
        ///             "cin": "string"
        ///         }
        ///     }
        /// 
        /// </remarks>
        /// <param name="request">Tenant Details</param>
        /// <returns>Returns Tenant Details</returns>
        /// <response code="200">Returns Tenant Details</response>
        [HttpPost]
        [ProducesResponseType(typeof(TenantResponse), Status200OK)]
        public async Task<ApiResponse> CreateTenant([FromBody] CreateTenantRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }

            try
            {
                var result = await _tenantService.CreateTenant(request);
                return new ApiResponse("Tenant created successfully", result, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Creates Tenant Onboard Comments
        /// </summary>
        /// <remarks>
        /// Sample Response:
        /// 
        ///     POST /tenant
        ///     {
        ///         "message": "Onboard Comment created successfully",
        ///         "result": true/false
        ///     }
        /// 
        /// </remarks>
        /// <param name="request">Tenant Details</param>
        /// <returns>Returns Tenant Details</returns>
        /// <response code="200">Returns Tenant Details</response>
        [HttpPost("onboard/comment")]
        [ProducesResponseType(typeof(TenantResponse), Status200OK)]
        public async Task<ApiResponse> CreateOnboardComment([FromBody] CreateTenantOnboardCommentRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }

            try
            {
                var result = await _tenantService.CreateOnboardComment(request);
                return new ApiResponse("Onboard Comment created successfully", result, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        #endregion

        #region CRUD - R

        /// <summary>
        /// Gets Tenant Details
        /// </summary>
        /// <remarks>
        /// Sample Response:
        /// 
        ///     GET /tenant/{id}
        ///     {
        ///         "message": "Tenant feteched successfully",
        ///         "result": {
        ///             "id": "9c441515-0820-4620-9dd6-97bcb1727248",
        ///             "contact": "string",
        ///             "address": "string",
        ///             "email": "string",
        ///             "employeesCount": 0,
        ///             "cin": "string"
        ///         }
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">TenantId</param>
        /// <returns>Returns Tenant Details</returns>
        /// <response code="200">Returns Tenant Details</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TenantResponse), Status200OK)]
        public async Task<ApiResponse> Get([FromRoute] string id)
        {
            try
            {
                var result = await _tenantService.Get(id);
                return new ApiResponse("Tenant feteched successfully", result, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Gets Tenants Status Details
        /// </summary>
        /// <remarks>
        /// Sample Response:
        /// 
        ///     GET /tenant/status
        ///     {
        ///         "message": "Data feteched successfully",
        ///         "result": {
        ///             "registratedCount": 1,
        ///             "kycInProgressCount": 0,
        ///             "approvedCount": 0,
        ///             "rejectedCount": 0
        ///         }
        ///     }
        /// 
        /// </remarks>
        /// <returns>Returns Tenants Status Details</returns>
        /// <response code="200">Returns Tenants Status Details</response>
        [HttpGet("status")]
        [ProducesResponseType(typeof(TenantStatusResponse), Status200OK)]
        public async Task<ApiResponse> GetTenantDashboard()
        {
            try
            {
                var result = await _tenantService.GetTenantStatusCount();
                return new ApiResponse("Data feteched successfully", result, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Gets Holders Status Details
        /// </summary>
        /// <remarks>
        /// Sample Response:
        /// 
        ///     GET /tenant/holder/status
        ///     {
        ///         "message": "Data feteched successfully",
        ///         "result": {
        ///             "invitionSentCount": 1,
        ///             "registeredCount": 2,
        ///             "kyeCompletedCount": 1
        ///         }
        ///     }
        /// 
        /// </remarks>
        /// <returns>Returns Holders Status Details</returns>
        /// <response code="200">Returns Holders Status Details</response>
        [HttpGet("holder/status")]
        [ProducesResponseType(typeof(HoldersStatusResponse), Status200OK)]
        public ApiResponse GetHoldersStatusCount()
        {
            try
            {
                var result = _tenantService.GetHoldersStatusCount();
                return new ApiResponse("Data feteched successfully", result, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Gets Tenants By filters
        /// </summary>
        /// <remarks>
        /// Sample Response:
        /// 
        ///     GET /tregistered/paged?PageNumber=1{seperator}PageSize=25{seperator}IncludeCount=true{seperator}Name=lovaraju{seperator}CIN=cin{seperator}FromDate=null{seperator}ToDate=null
        ///     {
        ///         "message": "Data feteched successfully",
        ///         "isError": false,
        ///         "result": [
        ///             {
        ///                 "id": "9c441515-0820-4620-9dd6-97bcb1727248",
        ///                 "contact": "string",
        ///                 "address": "string",
        ///                 "email": "string",
        ///                 "employeesCount": 0,
        ///                 "cin": "string"
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <param name="urlQueryParameters">Paging Query Parameters</param>
        /// <param name="filters">Filters</param>
        /// <returns>Returns List of Tenants</returns>
        /// <response code="200">Returns List of Tenants</response>
        [HttpGet("registered/paged")]
        [ProducesResponseType(typeof(TenantResponse), Status200OK)]
        public async Task<ApiResponse> GetOnboardPendingTenantsPages([FromQuery] UrlQueryParameters urlQueryParameters, [FromQuery] TenantFilter filters)
        {
            try
            {
                var result = await _tenantService.GetOnboardPendingTenantsPages(urlQueryParameters.PageNumber, urlQueryParameters.PageSize, filters, urlQueryParameters.IncludeCount);
                if (result != null)
                {
                    var metadata = new Pagination
                    {
                        PageNumber = result.PageIndex,
                        PageSize = result.PageSize,
                        TotalRecords = result.TotalCount,
                    };

                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
                    return new ApiResponse("Data Fetched Succesfully", result, Status200OK);
                }
                else
                    throw new ApiException("No Data Fetched Some internal Error");
            }
            catch(Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Get Tenant Onboard Comments
        /// </summary>
        /// <remarks>
        /// Sample Response:
        /// 
        ///     POST /tenant
        ///     {
        ///         "message": "Onboard Comments fetched successfully",
        ///         "result": [
        ///             {
        ///               "id": "b9e76bfb-67f4-44e9-8ec0-1902bd3cdcff",
        ///               "comment": "string",
        ///               "tenantId": "65182561-cdb5-410f-a72d-c7c5fc4cd319",
        ///               "createdDate": "2021-02-16T07:02:10.303",
        ///               "updatedDate": "2021-02-16T07:02:10.303"
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns>Returns Tenant Details</returns>
        /// <response code="200">Returns Tenant Details</response>
        [HttpGet("onboard/comment/{tenantId}")]
        [ProducesResponseType(typeof(IEquatable<TenantOnboardCommentResponse>), Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ApiResponse> CreateOnboardComment([FromRoute] string tenantId)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }

            try
            {
                var result = await _tenantService.GetTenantOnboardComments(tenantId);
                return new ApiResponse("Onboard Comments fetched successfully", result, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        #endregion

        #region CRUD - U

        /// <summary>
        /// Updates Tenant Details
        /// </summary>
        /// <remarks>
        /// Sample Response:
        /// 
        ///     PUT /tenant
        ///     {
        ///         "message": "Tenant updated successfully",
        ///         "result": true
        ///     }
        /// 
        /// </remarks>
        /// <param name="request">Tenant Details</param>
        /// <returns>Returns true</returns>
        /// <response code="200">Returns true</response>
        [HttpPut]
        [ProducesResponseType(typeof(bool), Status200OK)]
        public async Task<ApiResponse> UpdateTenant([FromBody] UpdateTenantRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }

            try
            {
                var result = await _tenantService.UpdateTenant(request);
                return new ApiResponse("Tenant updated successfully", result, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        #endregion
    }
}