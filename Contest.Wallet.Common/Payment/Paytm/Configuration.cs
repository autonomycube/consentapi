using Consent.Common.Configuration.Http;
using Consent.Common.Constants;
using Consent.Common.Payment.Paytm.Services;
using Consent.Common.Payment.Paytm.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Consent.Common.Payment.Paytm
{
    public static class Configuration
    {
        public static IServiceCollection AddPaytmPaymentGateway(this IServiceCollection services, IConfiguration config)
        {
            var paytmConfig = new PaytmConfig();
            var policyConfigs = new HttpClientPolicyConfiguration();
            config.Bind("PaytmConfig", paytmConfig);
            config.Bind("HttpClientPolicies", policyConfigs);

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(policyConfigs.RetryTimeoutInSeconds));

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(r => r.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(policyConfigs.RetryCount, _ => TimeSpan.FromMilliseconds(policyConfigs.RetryDelayInMs));

            var circuitBreakerPolicy = HttpPolicyExtensions
               .HandleTransientHttpError()
               .CircuitBreakerAsync(policyConfigs.MaxAttemptBeforeBreak, TimeSpan.FromSeconds(policyConfigs.BreakDurationInSeconds));

            var noOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

            services.AddHttpClient<IPaytmApiService, PaytmApiService>(client =>
            {
                client.BaseAddress = new Uri(paytmConfig.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpContentMediaTypes.JSON));
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(policyConfigs.HandlerTimeoutInMinutes))
            .AddPolicyHandler(request => request.Method == HttpMethod.Get ? retryPolicy : noOpPolicy)
            .AddPolicyHandler(timeoutPolicy)
            .AddPolicyHandler(circuitBreakerPolicy);

            // Paytm Configuration
            services.Configure<PaytmConfig>(config.GetSection(nameof(PaytmConfig)));

            return services;
        }
    }
}
