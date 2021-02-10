using Consent.Common.Data.UOW;
using Consent.Common.Data.UOW.Abstract;
using Consent.Api.Contracts;
using Consent.Api.Payment.Data.DbContexts;
using Consent.Api.Payment.Services;
using Consent.Api.Payment.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Payment.Infrastructure.Installers
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork<PaymentDbContext>, UnitOfWork<PaymentDbContext>>();
            services.AddTransient<IPaymentService, PaymentService>();
        }
    }
}
