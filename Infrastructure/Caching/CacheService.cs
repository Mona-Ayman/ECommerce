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

        public T GetData<T, TKey>(TKey key)
        {
            return memoryCache.Get<T>(key);
        }

        public void RemoveData(CachingCategory key)
        {
            //if (string.IsNullOrWhiteSpace(key))
            //    return;

            memoryCache.Remove(key);
        }

        public bool SetData<T, TKey>(TKey key, T value, DateTimeOffset expirationTime)
        {
            memoryCache.Set(key, value, expirationTime);
            return true;
        }

        public List<T> AddCachingKeys<T>(CachingCategory key, T value)
        {
            List<T> cachingKeys = memoryCache.Get<List<T>>(key) ?? new List<T>();

            cachingKeys.Add(value);
            return cachingKeys;
        }

        public string GenerateKey(CachingCategory prefix, string key)
        {
            return $"{prefix} {key}";
        }

        #endregion
    }
}
