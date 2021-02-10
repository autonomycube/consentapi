using AutoMapper;
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

        private readonly IUserService _testService;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AuthController(IUserService testService,
            IMapper mapper,
            ILogger<AuthController> logger)
        {
            _testService = testService
                ?? throw new ArgumentNullException(nameof(testService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - R

        /// <summary>
        /// Test Method
        /// </summary>
        /// <returns>Returns string array</returns>
        /// <response code="200">Returns string array</response>
        [HttpGet]
        [ProducesResponseType(typeof(string[]), Status200OK)]
        public async Task<string[]> Get()
        {
            return await _testService.Get();
        }

        #endregion
    }
}