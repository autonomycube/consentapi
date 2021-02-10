using AutoMapper;
using AutoWrapper.Wrappers;
using Consent.Api.Tenant.Services.Abstract;
using Consent.Api.Tenant.Services.DTO.Response;
using Consent.Api.Tenant.Services.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        ///             "id": "xxx",
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
        ///         }
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">TenantId</param>
        /// <returns>Returns Tenant Details</returns>
        /// <response code="200">Returns Tenant Details</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TenantResponse), Status200OK)]
        public async Task<ApiResponse> Get([FromRoute]string id)
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