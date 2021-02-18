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

        private readonly IFileUploadService _fileUploadService;
        private readonly ILogger<UtilityController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UtilityController(IFileUploadService fileUploadService,
            IMapper mapper,
            ILogger<UtilityController> logger)
        {
            _fileUploadService = fileUploadService
                ?? throw new ArgumentNullException(nameof(fileUploadService));
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
        ///     POST /utility/fileupload/key
        ///     {
        ///         "message": "File uploaded successfully",
        ///         "result": {
        ///             "fileUrl": "key/sample.pdf"
        ///         }
        ///     }
        /// 
        /// </remarks>
        /// <param name="file">File blob</param>
        /// <param name="key">S3 Key</param>
        /// <returns>Returns true</returns>
        /// <response code="200">Returns true</response>
        [HttpPost("fileupload/{key}")]
        [ProducesResponseType(typeof(bool), Status200OK)]
        public async Task<ApiResponse> UploadFile(IFormFile file, [FromRoute] string key)
        {
            if (file == null)
            {
                return new ApiResponse(Status400BadRequest, "No file is attached.");
            }

            try
            {
                var result = await _fileUploadService.Upload(file, key);
                return new ApiResponse("File uploaded successfully.", result);
            }
            catch (Exception e)
            {
                _logger.LogError("UploadFile - Exception: " + e.Message);
                throw new ApiException(e.Message);
            }
        }

        /// <summary>
        /// Deletes S3Key
        /// </summary>
        /// <remarks>
        /// Sample Response:
        /// 
        ///     POST /utility/deletes3key/key
        ///     {
        ///         "message": "File uploaded successfully",
        ///         "result": true
        ///     }
        /// 
        /// </remarks>
        /// <param name="key">S3 Key</param>
        /// <returns>Returns TRUE/FALSE</returns>
        /// <response code="200">Returns TRUE/FALSE</response>
        [HttpDelete("deletes3key/{key}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<ApiResponse> DeleteS3Key([FromRoute] string key)
        {
            try
            {
                var result = await _fileUploadService.DeleteS3file(key);
                return new ApiResponse("Data Fetched Succesfully", result, Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteS3Key - Exception: {0}", ex.Message);
                throw new ApiException("No Data Deleted Some internal Error");
            }
        }

        #endregion
    }
}