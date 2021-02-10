using Consent.Api.Contracts;
using Consent.Api.Payment.Data.Repositories;
using Consent.Api.Payment.Data.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Payment.Infrastructure.Installers
{
    internal class RegisterRepositories : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Register Interface Mappings for Repositories
            services.AddTransient<IPaymentRepository, PaymentRepository>();
        }
    }
}
