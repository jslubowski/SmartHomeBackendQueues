using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.DAL.Data;
using SmartHome.Services.External;

namespace SmartHome.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddHostedService<RabbitService>();
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
    }
}
