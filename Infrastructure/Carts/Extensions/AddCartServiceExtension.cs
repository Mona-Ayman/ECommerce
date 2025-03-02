using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Carts.Extensions
{
    public static class AddCartServiceExtension
    {
        public static IServiceCollection AddCartService(this IServiceCollection services)
        {
            services.AddScoped<IUpdateCartPriceService, UpdateCartPriceService>();
            services.AddHostedService<UpdateCartPriceService>();

            return services;
        }
    }
}
