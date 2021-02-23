using Consent.Common.Data.UOW;
using Consent.Common.Data.UOW.Abstract;
using Consent.Api.Contracts;
using Consent.Api.Auth.Data.DbContexts;
using Consent.Api.Auth.Services;
using Consent.Api.Auth.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Auth.Infrastructure.Installers
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
