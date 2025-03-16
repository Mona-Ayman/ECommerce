using Domain._Base.Interfaces;
using Domain.Carts.Interfaces;
using Domain.ProductRates.Interfaces;
using Domain.Products.Interfaces;
using Domain.Users.Interfaces;
using Infrastructure.Persistence._Base;
using Infrastructure.Persistence.Context.ECommerce.Data;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Extensions
{
    public static class AddPersistenceExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ECommerceContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ECommerceContext") ?? throw new InvalidOperationException("Connection string 'SchoolContext' not found.")));

            //services.AddDbContext<ECommerceContext>(options =>
            //   options.UseSqlServer(configuration.GetConnectionString("IdentityContext") ?? throw new InvalidOperationException("Connection string 'SchoolContext' not found.")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductRateRepository, ProductRateRepository>();
            services.AddScoped<IProductCachingRepository, ProductCachingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
