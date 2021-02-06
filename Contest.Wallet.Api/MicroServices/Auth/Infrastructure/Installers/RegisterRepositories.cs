using Contest.Wallet.Api.Contracts;
using Contest.Wallet.Api.Auth.Data.Repositories;
using Contest.Wallet.Api.Auth.Data.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contest.Wallet.Api.Auth.Infrastructure.Installers
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
