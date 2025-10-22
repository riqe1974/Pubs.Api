using Microsoft.EntityFrameworkCore;
using Pubs.API.Data;
using Pubs.API.Interfaces;
using Pubs.API.Services;

namespace Pubs.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PubsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<ITitleService, TitleService>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }
    }
}