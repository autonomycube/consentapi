using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace Contest.Wallet.Common.Cache.Abstract
{
    public interface IRedisCache
    {
        /// <summary>
        /// Get cache value
        /// </summary>
        /// <param name="cacheKey">Caching key</param>
        /// <returns>string</returns>
        string Get(string cacheKey);
        /// <summary>
        /// Get or set cache value
        /// </summary>
        /// <param name="cacheKey">Caching key</param>
        /// <param name="func">This function will trigger only if cache is empty</param>
        /// <param name="options">Expiry of cache</param>
        /// <returns>string</returns>
        string Get(string cacheKey, Func<string> func, Action<DistributedCacheEntryOptions> options);
        /// <summary>
        /// Get cache value
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="cacheKey">Caching key</param>
        /// <returns>T</returns>
        T Get<T>(string cacheKey) where T : class;
        /// <summary>
        /// Get or set cache value
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="cacheKey">Caching key</param>
        /// <param name="func">This function will trigger only if cache is empty</param>
        /// <param name="options">Expiry of cache</param>
        /// <returns>T</returns>
        T Get<T>(string cacheKey, Func<T> func, Action<DistributedCacheEntryOptions> options) where T : class;
        /// <summary>
        /// Get cache value
        /// </summary>
        /// <param name="cacheKey">Caching key</param>
        /// <returns>Task<string></returns>
        Task<string> GetAsync(string cacheKey);
        /// <summary>
        /// Get or set cache value
        /// </summary>
        /// <param name="cacheKey">Caching key</param>
        /// <param name="func">This function will trigger only if cache is empty</param>
        /// <param name="options">Expiry of cache</param>
        /// <returns>Task<string></returns>
        Task<string> GetAsync(string cacheKey, Func<Task<string>> func, Action<DistributedCacheEntryOptions> options);
        /// <summary>
        /// Get cache value
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="cacheKey">Caching key</param>
        /// <returns>Task<T></returns>
        Task<T> GetAsync<T>(string cacheKey) where T : class;
        /// <summary>
        /// Get or set cache value
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="cacheKey">Caching key</param>
        /// <param name="func">This function will trigger only if cache is empty</param>
        /// <param name="options">Expiry of cache</param>
        /// <returns>Task<T></returns>
        Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> func, Action<DistributedCacheEntryOptions> options) where T : class;
        /// <summary>
        /// Refresh cache value
        /// </summary>
        /// <param name="cacheKey">Caching key</param>
        void Refresh(string cacheKey);
        /// <summary>
        /// Refresh cache value
        /// </summary>
        /// <param name="cacheKey">Caching key</param>
        /// <returns>Task</returns>
        Task RefreshAsync(string cacheKey);
        /// <summary>
        /// Remove cache value
        /// </summary>
        /// <param name="cacheKey">Caching key</param>
        void Remove(string cacheKey);
        /// <summary>
        /// Remove cache value
        /// </summary>
        /// <param name="cacheKey">Caching key</param>
        /// <returns>Task</returns>
        Task RemoveAsync(string cacheKey);
        /// <summary>
        /// Set cache value
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="cacheKey">Caching key</param>
        /// <param name="value">Value to set</param>
        /// <param name="options">Expiry of cache</param>
        void Set<T>(string cacheKey, T value, Action<DistributedCacheEntryOptions> options) where T : class;
        /// <summary>
        /// Set cache value
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="cacheKey">Caching key</param>
        /// <param name="value">Value to set</param>
        /// <param name="options">Expiry of cache</param>
        /// <returns>Task</returns>
        Task SetAsync<T>(string cacheKey, T value, Action<DistributedCacheEntryOptions> options) where T : class;
        /// <summary>
        /// Set cache value
        /// </summary>
        /// <param name="cacheKey">Caching key</param>
        /// <param name="value">Value to set</param>
        /// <param name="options">Expiry of cache</param>
        void SetString(string cacheKey, string value, Action<DistributedCacheEntryOptions> options);
        /// <summary>
        /// Set cache value
        /// </summary>
        /// <param name="cacheKey">Caching key</param>
        /// <param name="value">Value to set</param>
        /// <param name="options">Expiry of cache</param>
        /// <returns>Task</returns>
        Task SetStringAsync(string cacheKey, string value, Action<DistributedCacheEntryOptions> options);
    }
}
