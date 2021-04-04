using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Filters;
using AutoWrapper.Wrappers;
using Consent.Api.MicroServices.Payment.DTO.Response;
using Consent.Api.Payment.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
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

        private readonly string _paymentClientUrl;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public PaymentController(IPaymentService paymentService,
            IMapper mapper,
            ILogger<PaymentController> logger,
            IConfiguration config)
        {
            _paymentService = paymentService
                ?? throw new ArgumentNullException(nameof(paymentService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
            _paymentClientUrl = config["PaymentClientUrl"];
        }

        #endregion

        #region CRUD - R

        /// <summary>
        /// Generates Payment CheckSum
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <param name="customerId">Customer Id</param>
        /// <returns>Returns CheckSum</returns>
        /// <response code="200">Returns CheckSum</response>
        [HttpGet("CheckSum/{orderId}/{customerId}")]
        [ProducesResponseType(typeof(string), Status200OK)]
        public async Task<ApiResponse> GenerateCheckSum([FromRoute] string orderId, [FromRoute] string customerId)
        {
            try
            {
                var result = await _paymentService.GenerateCheckSum(orderId, customerId);
                return new ApiResponse("CheckSum generated successfully.", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse(ex);
            }
        }

        /// <summary>
        /// Gets PaymentTransaction 
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <returns>Returns PaymentTransaction</returns>
        /// <response code="200">Returns PaymentTransaction</response>
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(PaymentConfirmationResponse), Status200OK)]
        public async Task<ApiResponse> GetPaymentTransaction([FromRoute] string orderId)
        {
            try
            {
                var result = await _paymentService.GetPaymentTransaction(orderId);
                return new ApiResponse("Data fetched successfully.", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse(ex);
            }
        }

        #endregion

        #region CRUD - U

        /// <summary>
        /// Confirms Payment Status
        /// </summary>
        /// <returns>Redirects to Client App</returns>
        /// <response code="200">Redirects to Client App</response>
        [AutoWrapIgnore]
        [HttpPost("Confirmation")]
        public async Task<ActionResult> PaymentConfirmation()
        {
            byte[] buf = new byte[4096];
            int count = Request.Body.Read(buf, 0, 4096);
            string jsonPayload = Encoding.UTF8.GetString(buf, 0, count);
            string orderId = await _paymentService.UpdatePaymentStatus(jsonPayload);
            return Redirect(string.Format(_paymentClientUrl, orderId));
        }

        #endregion
    }
}