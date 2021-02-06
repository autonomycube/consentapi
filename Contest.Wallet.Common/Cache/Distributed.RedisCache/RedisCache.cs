using Contest.Wallet.Common.Cache.Abstract;
using Contest.Wallet.Common.Cache.Distributed.RedisCache.Helper;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace Contest.Wallet.Common.Cache.Distributed.RedisCache
{
    internal sealed class RedisCache : IRedisCache
    {
        private readonly IDistributedCache distributedCache;

        public RedisCache(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public T Get<T>(string cacheKey, Func<T> func, Action<DistributedCacheEntryOptions> options) where T : class
        {
            try
            {
                byte[] bytes = distributedCache.Get(cacheKey);
                if (bytes == null)
                {
                    T result = func.Invoke();
                    Set(cacheKey, result, options);
                    return result;
                }
                return bytes.FromByteArray<T>();
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public string Get(string cacheKey, Func<string> func, Action<DistributedCacheEntryOptions> options)
        {
            try
            {
                string result = distributedCache.GetString(cacheKey);
                if (string.IsNullOrEmpty(result))
                {
                    result = func.Invoke();
                    SetString(cacheKey, result, options);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public T Get<T>(string cacheKey) where T : class
        {
            try
            {
                byte[] bytes = distributedCache.Get(cacheKey);
                return bytes.FromByteArray<T>();
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public string Get(string cacheKey)
        {
            try
            {
                return distributedCache.GetString(cacheKey);
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public async Task<string> GetAsync(string cacheKey)
        {
            try
            {
                return await distributedCache.GetStringAsync(cacheKey);
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public async Task<string> GetAsync(string cacheKey, Func<Task<string>> func, Action<DistributedCacheEntryOptions> options)
        {
            try
            {
                string result = await distributedCache.GetStringAsync(cacheKey);
                if (string.IsNullOrEmpty(result))
                {
                    result = await func.Invoke();
                    await SetStringAsync(cacheKey, result, options);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public async Task<T> GetAsync<T>(string cacheKey) where T : class
        {
            try
            {
                byte[] bytes = await distributedCache.GetAsync(cacheKey);
                return bytes.FromByteArray<T>();
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public async Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> func, Action<DistributedCacheEntryOptions> options) where T : class
        {
            try
            {
                byte[] bytes = await distributedCache.GetAsync(cacheKey);
                if (bytes == null)
                {
                    T result = await func.Invoke();
                    await SetAsync(cacheKey, result, options);
                    return result;
                }
                return bytes.FromByteArray<T>();
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public void Refresh(string cacheKey)
        {
            try
            {
                distributedCache.Refresh(cacheKey);
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public async Task RefreshAsync(string cacheKey)
        {
            try
            {
                await distributedCache.RefreshAsync(cacheKey);
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public void Remove(string cacheKey)
        {
            try
            {
                distributedCache.Remove(cacheKey);
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public async Task RemoveAsync(string cacheKey)
        {
            try
            {
                await distributedCache.RemoveAsync(cacheKey);
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public void Set<T>(string cacheKey, T value, Action<DistributedCacheEntryOptions> options) where T : class
        {
            try
            {
                if (value == null) return;
                distributedCache.Set(cacheKey, value.ToByteArray(), options.GetValues());
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public async Task SetAsync<T>(string cacheKey, T value, Action<DistributedCacheEntryOptions> options) where T : class
        {
            try
            {
                if (value == null) return;
                await distributedCache.SetAsync(cacheKey, value.ToByteArray(), options.GetValues());
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public void SetString(string cacheKey, string value, Action<DistributedCacheEntryOptions> options)
        {
            try
            {
                if (value == null) return;
                distributedCache.SetString(cacheKey, value, options.GetValues());
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }

        public async Task SetStringAsync(string cacheKey, string value, Action<DistributedCacheEntryOptions> options)
        {
            try
            {
                if (value == null) return;
                await distributedCache.SetStringAsync(cacheKey, value, options.GetValues());
            }
            catch (Exception ex)
            {
                throw new RedisCacheException(ex.Message, ex.InnerException);
            }
        }
    }
}
