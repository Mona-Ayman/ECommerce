namespace Application.Services
{
    public interface ICacheService
    {
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        T GetData<T>(string key);
        void RemoveData(string key);
    }
}
