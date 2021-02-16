using Consent.Api.Contracts;
using Consent.Api.Notification.Data.Repositories;
using Consent.Api.Notification.Data.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Notification.Infrastructure.Installers
{
    internal class RegisterRepositories : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Register Interface Mappings for Repositories
            services.AddTransient<IEmailRepository, EmailRepository>();
            services.AddTransient<IOtpTrackerRepository, OtpTrackerRepository>();
            services.AddTransient<ISmsRepository, SmsRepository>();
        }
    }
}
