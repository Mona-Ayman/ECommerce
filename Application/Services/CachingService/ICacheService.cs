using Application.Services.CachingService.Enums;

namespace Application.Services.CachingService
{
    public interface ICacheService
    {
        bool SetData<T, TKey>(TKey key, T value, DateTimeOffset expirationTime);
        T GetData<T, TKey>(TKey key);
        string GenerateKey(CachingCategory prefix, string key);
        void RemoveData(CachingCategory key);
        List<T> AddCachingKeys<T>(CachingCategory key, T value);
    }
}
