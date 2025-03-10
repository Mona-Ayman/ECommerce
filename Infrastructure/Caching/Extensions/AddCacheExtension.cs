using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Caching.Extensions
{
    public static class AddCacheExtension
    {
        public static IServiceCollection AddCacheService(this IServiceCollection services)
        {
            services.AddScoped<ICacheService, CacheService>();

            return services;
        }
    }
}
