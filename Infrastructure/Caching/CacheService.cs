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

        public Task<T> GetData<T>(CachingCategory category, string key)
        {
            string cachingKey = $"{category}_{key}";
            return Task.FromResult(memoryCache.Get<T>(cachingKey));
        }

        public async Task RemoveData(CachingCategory categoryKey)
        {
            List<string> keys = memoryCache.Get<List<string>>(categoryKey);
            if (keys != null)
                foreach (string key in keys)
                    memoryCache.Remove(key);

            memoryCache.Remove(categoryKey);
        }

        public async Task<bool> SetData<T>(CachingCategory category, string key, T value, DateTimeOffset expirationTime)
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
