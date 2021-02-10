using Consent.Api.Contracts;
using Consent.Api.Auth.Data.Repositories;
using Consent.Api.Auth.Data.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Auth.Infrastructure.Installers
{
    internal class RegisterRepositories : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Register Interface Mappings for Repositories
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
