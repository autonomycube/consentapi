using Microsoft.Extensions.Caching.Distributed;
using System;

namespace Consent.Common.Cache.Distributed.RedisCache.Helper
{
    internal static class ActionConversion
    {
        public static DistributedCacheEntryOptions GetValues(this Action<DistributedCacheEntryOptions> action)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            action?.Invoke(options);
            return options;
        }
    }
}
