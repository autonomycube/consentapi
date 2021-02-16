using Consent.Common.Data.UOW;
using Consent.Common.Data.UOW.Abstract;
using Consent.Api.Contracts;
using Consent.Api.Notification.Data.DbContexts;
using Consent.Api.Notification.Services;
using Consent.Api.Notification.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Notification.Infrastructure.Installers
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork<NotificationDbContext>, UnitOfWork<NotificationDbContext>>();
            services.AddTransient<ISmsService, SmsService>();
        }
    }
}
