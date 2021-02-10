using Consent.Common.ApplicationMonitoring;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Api.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Infrastructure.Installers
{
    public class RegisterMonitoringResources : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            // register logger
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}