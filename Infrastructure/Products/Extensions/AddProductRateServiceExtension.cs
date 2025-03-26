using Application.Services.ChannelService;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;

namespace Infrastructure.Products.Extensions
{
    public static class AddProductRateServiceExtension
    {
        public static IServiceCollection AddProductRateService(this IServiceCollection services)
        {
            services.AddScoped<IChannelUpdateRateService, ChannelUpdateRateService>();
            services.AddHostedService<UpdateProductRateService>();
            services.AddSingleton(Channel.CreateUnbounded<(Guid, decimal?, decimal)>());
            return services;
        }
    }
}
