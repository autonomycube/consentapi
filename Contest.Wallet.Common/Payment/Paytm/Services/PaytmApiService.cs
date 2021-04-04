using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Common.Constants;
using Consent.Common.Helpers.Abstract;
using Consent.Common.Payment.Paytm.Services.Abstract;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PaytmcheckSum = Paytm.Checksum;

namespace Consent.Common.Payment.Paytm.Services
{
    public class PaytmApiService : IPaytmApiService
    {
        #region Private Variables

        private readonly HttpClient _httpClient;
        private readonly PaytmConfig _paytmConfig;
        private readonly IBaseAuthHelper _baseAuthHelper;
        private readonly ILogger<PaytmApiService> _logger;

        #endregion

        #region Constructor

        public PaytmApiService(HttpClient httpClient,
            IOptions<PaytmConfig> paytmConfig,
            IBaseAuthHelper baseAuthHelper,
            ILogger<PaytmApiService> logger)
        {
            _httpClient = httpClient
                ?? throw new ArgumentNullException(nameof(httpClient));
            _baseAuthHelper = baseAuthHelper
                ?? throw new ArgumentNullException(nameof(baseAuthHelper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));

            _paytmConfig = paytmConfig.Value;
        }

        #endregion

        #region Public Methods

        public string GenerateCheckSum(string orderId)
        {
            Dictionary<string, string> paytmParams = new Dictionary<string, string>();

            paytmParams.Add("MID", _paytmConfig.MerchantID);
            paytmParams.Add("ORDER_ID", orderId);

            string paytmChecksum = PaytmcheckSum.CheckSum.GenerateCheckSum(_paytmConfig.MerchantKey, paytmParams);
            return paytmChecksum;
        }

        #endregion

        #region Private Methods

        private async Task<TResponse> Post<TResponse, TRequest>(string endPoint, TRequest dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, HttpContentMediaTypes.JSON);
            var httpResponse = await _httpClient.PostAsync(endPoint, content);
            var jsonString = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(jsonString);
        }

        #endregion
    }
}
