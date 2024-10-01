using GARP.Application.Interface.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace GARP.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAutoService, AutoService>();

            return services;
        }
    }
}
