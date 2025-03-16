using Application.Services.CachingService.Enums;

namespace Application.Services.CachingService
{
    public interface ICacheService
    {
        Task<bool> SetData<T>(CachingCategory category, string key, T value, DateTimeOffset expirationTime);
        Task<T> GetData<T>(CachingCategory category, string key);
        Task RemoveData(CachingCategory key);
    }
}
