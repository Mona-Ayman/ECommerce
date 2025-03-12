using Application.Services.CachingService;
using Application.Services.CachingService.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Caching
{
    public class CacheService : ICacheService
    {
        #region Fields

        private readonly IMemoryCache memoryCache;

        #endregion

        #region Constructors

        public CacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        #endregion

        #region Methods

        public T GetData<T>(CachingCategory category, string key)
        {
            string cachingKey = $"{category}_{key}";
            return memoryCache.Get<T>(cachingKey);
        }

        public void RemoveData(CachingCategory key)
        {
            memoryCache?.Remove(key);
        }

        public bool SetData<T>(CachingCategory category, string key, T value, DateTimeOffset expirationTime)
        {
            string cachingKey = $"{category}_{key}";
            memoryCache.Set(cachingKey, value, expirationTime);

            List<string> cachingKeys = memoryCache.Get<List<string>>(category) ?? new List<string>();
            cachingKeys.Add(cachingKey);
            memoryCache.Set(category, cachingKeys, expirationTime);

            return true;
        }

        #endregion
    }
}
