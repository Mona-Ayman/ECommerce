using Application.Services.ChannelService;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;

namespace Infrastructure.Carts.Extensions
{
    public static class AddCartServiceExtension
    {
        public static IServiceCollection AddCartService(this IServiceCollection services)
        {
            services.AddScoped<IChannelCartPriceChangeService, ChannelCartPriceChangeService>();
            services.AddHostedService<UpdateCartPriceService>();
            services.AddSingleton(Channel.CreateUnbounded<(Guid, decimal)>());
            return services;
        }
    }
}
