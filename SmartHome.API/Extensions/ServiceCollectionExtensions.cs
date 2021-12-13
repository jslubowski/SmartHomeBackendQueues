using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.BLL.Repositories;
using SmartHome.BLL.Services.Internal;
using SmartHome.DAL.Data;
using SmartHome.DAL.Repositories;
using SmartHome.Services.External;
using SmartHome.Services.Internal;

namespace SmartHome.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddHostedService<RabbitService>();
            services.AddScoped<ITemperatureService, TemperatureService>();
        }

        public static void AddPostgresContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PostgresDbContext>(options =>
            {
                options
                .UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"))
                .UseSnakeCaseNamingConvention();
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITemperatureRepository, TemperatureRepository>();
        }
    }
}
