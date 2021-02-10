using Consent.Common.Configuration;
using Consent.Common.Configuration.Options;
using Consent.Api.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Infrastructure.Installers
{
    internal class RegisterCaching : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Register Interface Mappings for Caching
            services.AddSingleton<CachingOptions>();
            services.AddSingleton<IAppSetting, AppSettings>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
        }
    }
}
