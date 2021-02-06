using Contest.Wallet.Common.Cache.Distributed.RedisCache;
using Contest.Wallet.Api.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contest.Wallet.Api.Infrastructure.Installers
{
    internal class RegisterRedisCache : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("RedisConfig");
            var connection = section.GetSection("ConnectionUrl");
            var instance = section.GetSection("InstanceName");
            // add redis cache
            services.AddRedisCache(options =>
            {
                options.Configuration = connection.Value;
                options.InstanceName = instance.Value;
                options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
                {
                    AbortOnConnectFail = false,
                    ConnectTimeout = 4000,
                    EndPoints =
                    {
                        { connection.Value }
                    },
                    AllowAdmin = true
                };
            });
        }
    }
}
