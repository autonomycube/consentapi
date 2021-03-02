using Consent.Api.Contracts;
using Consent.Api.Tenant.Data.Repositories;
using Consent.Api.Tenant.Data.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Tenant.Infrastructure.Installers
{
    internal class RegisterRepositories : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Register Interface Mappings for Repositories
            services.AddTransient<IInvitationsRepository, InvitationsRepository>();
            services.AddTransient<ITenantRepository, TenantRepository>();
            services.AddTransient<ITenantOnboardStatusRepository, TenantOnboardStatusRepository>();
        }
    }
}
