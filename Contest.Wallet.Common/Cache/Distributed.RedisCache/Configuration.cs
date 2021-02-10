using Consent.Common.Cache.Abstract;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Consent.Common.Cache.Distributed.RedisCache
{
    public static class Configuration
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection service, Action<RedisCacheOptions> action)
        {
            service.AddSingleton<IRedisCache, RedisCache>();
            return service.AddDistributedRedisCache(action);
        }
    }
}
