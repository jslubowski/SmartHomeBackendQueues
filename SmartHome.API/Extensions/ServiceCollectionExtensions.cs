using Microsoft.Extensions.DependencyInjection;
using SmartHome.Services.External;

namespace SmartHome.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddHostedService<RabbitService>();
        }
    }
}
