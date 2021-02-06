using Contest.Wallet.Common.Data.UOW;
using Contest.Wallet.Common.Data.UOW.Abstract;
using Contest.Wallet.Api.Contracts;
using Contest.Wallet.Api.Tenant.Data.DbContexts;
using Contest.Wallet.Api.Tenant.Services;
using Contest.Wallet.Api.Tenant.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contest.Wallet.Api.Tenant.Infrastructure.Installers
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork<IssuerDbContext>, UnitOfWork<IssuerDbContext>>();
            services.AddTransient<ITestService, TestService>();
        }
    }
}
