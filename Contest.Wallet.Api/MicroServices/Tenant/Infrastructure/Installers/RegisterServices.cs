using Consent.Common.Data.UOW;
using Consent.Common.Data.UOW.Abstract;
using Consent.Api.Contracts;
using Consent.Api.Tenant.Data.DbContexts;
using Consent.Api.Tenant.Services;
using Consent.Api.Tenant.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Tenant.Infrastructure.Installers
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork<TenantDbContext>, UnitOfWork<TenantDbContext>>();
            services.AddTransient<ITenantService, TenantService>();
        }
    }
}
