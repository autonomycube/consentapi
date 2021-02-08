using Contest.Wallet.Api.Contracts;
using Contest.Wallet.Api.Payment.Data.Repositories;
using Contest.Wallet.Api.Payment.Data.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contest.Wallet.Api.Payment.Infrastructure.Installers
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
