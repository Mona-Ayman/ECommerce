using Application.Services.CachingService.Enums;

namespace Application.Services.CachingService
{
    public interface ICacheService
    {
        bool SetData<T>(CachingCategory category, string key, T value, DateTimeOffset expirationTime);
        T GetData<T>(CachingCategory category, string key);
        void RemoveData(CachingCategory key);
    }
}
