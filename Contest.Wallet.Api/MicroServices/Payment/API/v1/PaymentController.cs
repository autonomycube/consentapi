using AutoMapper;
using Consent.Api.Payment.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Consent.Api.Payment.API.v1
{
    [Produces("application/json")]
    [Route("api/v1/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        #region Private Variables

        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public PaymentController(IPaymentService paymentService,
            IMapper mapper,
            ILogger<PaymentController> logger)
        {
            _paymentService = paymentService
                ?? throw new ArgumentNullException(nameof(paymentService));
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
            return await _paymentService.Get();
        }

        #endregion
    }
}