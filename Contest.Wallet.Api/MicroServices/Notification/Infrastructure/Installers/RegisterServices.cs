using Contest.Wallet.Common.Data.UOW;
using Contest.Wallet.Common.Data.UOW.Abstract;
using Contest.Wallet.Api.Contracts;
using Contest.Wallet.Api.Notification.Data.DbContexts;
using Contest.Wallet.Api.Notification.Services;
using Contest.Wallet.Api.Notification.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contest.Wallet.Api.Notification.Infrastructure.Installers
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork<NotificationDbContext>, UnitOfWork<NotificationDbContext>>();
            services.AddTransient<ISMSService, SMSService>();
        }
    }
}
