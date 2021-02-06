using Contest.Wallet.Common.Data.UOW;
using Contest.Wallet.Common.Data.UOW.Abstract;
using Contest.Wallet.Api.Contracts;
using Contest.Wallet.Api.Auth.Data.DbContexts;
using Contest.Wallet.Api.Auth.Services;
using Contest.Wallet.Api.Auth.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contest.Wallet.Api.Auth.Infrastructure.Installers
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork<AuthDbContext>, UnitOfWork<AuthDbContext>>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
