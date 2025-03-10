using Application.Services.CachingService;
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

        public T GetData<T>(string key)
        {
            return memoryCache.Get<T>(key);
        }

        public void RemoveData(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;

            memoryCache.Remove(key);
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            if (string.IsNullOrWhiteSpace(key))
                return false;

            memoryCache.Set(key, value, expirationTime);
            return true;
        }

        #endregion
    }
}
