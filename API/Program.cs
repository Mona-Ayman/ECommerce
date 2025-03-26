using API.Extensions;
using API.Middlewares;
using Application.Extensions;
using Infrastructure.Caching.Extensions;
using Infrastructure.Carts.Extensions;
using Infrastructure.Identity.Extensions;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Products.Extensions;
using Microsoft.Extensions.Options;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAPI(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddIdentityService();
            builder.Services.AddCartService();
            builder.Services.AddProductRateService();
            builder.Services.AddCacheService();
            builder.Services.AddRepositories(builder.Configuration);
            builder.Services.AddControllersWithViews();
            builder.Services.AddLocalizationServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}