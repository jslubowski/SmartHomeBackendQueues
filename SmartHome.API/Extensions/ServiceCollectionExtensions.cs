using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.BLL.EventProcessing;
using SmartHome.BLL.Repositories.Internal;
using SmartHome.BLL.Services.External;
using SmartHome.BLL.Services.Internal;
using SmartHome.DAL.Data;
using SmartHome.DAL.Repositories;
using SmartHome.Services.EventProcessing;
using SmartHome.Services.External;
using SmartHome.Services.Internal;

namespace SmartHome.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddHostedService<MessageBusListener>();
            services.AddScoped<ISensorService, SensorService>();
            services.AddSingleton<IEventProcessor, EventProcessor>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
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
            services.AddScoped<ISensorRepository, SensorRepository>();
        }
    }
}
