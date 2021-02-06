using Contest.Wallet.Common.ApplicationMonitoring;
using Contest.Wallet.Common.ApplicationMonitoring.Abstract;
using Contest.Wallet.Api.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contest.Wallet.Api.Infrastructure.Installers
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