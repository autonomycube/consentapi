using AutoMapper;
using AutoWrapper.Wrappers;
using Consent.Api.Utility.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Consent.Api.Utility.API.v1
{
    [Produces("application/json")]
    [Route("api/v1/utility")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        #region Private Variables

        private readonly IFileUploadService _testService;
        private readonly ILogger<UtilityController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UtilityController(IFileUploadService testService,
            IMapper mapper,
            ILogger<UtilityController> logger)
        {
            _testService = testService
                ?? throw new ArgumentNullException(nameof(testService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - C

        /// <summary>
        /// Uploads File
        /// </summary>
        /// <remarks>
        /// Sample Response:
        /// 
        ///     POST /utility/fileupload
        ///     {
        ///         "message": "File uploaded successfully",
        ///         "result": true
        ///     }
        /// 
        /// </remarks>
        /// <param name="file">S3 File</param>
        /// <returns>Returns true</returns>
        /// <response code="200">Returns true</response>
        [HttpPost("fileupload")]
        [ProducesResponseType(typeof(bool), Status200OK)]
        public async Task<ApiResponse> UploadFile(IFormFile file)
        {
            try
            {
                return new ApiResponse("File uploaded successfully", null, Status200OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        #endregion
    }
}