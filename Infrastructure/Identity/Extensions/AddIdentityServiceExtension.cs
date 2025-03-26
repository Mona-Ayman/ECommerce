using Application.Services.TokenService;
using Domain.Users;
using Infrastructure.Persistence.Context.ECommerce.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity.Extensions
{
    public static class AddIdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ECommerceContext>();

            return services;
        }
    }
}
