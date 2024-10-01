using GARP.Application.Interface.Persistence;
using GARP.Persistence.Contexts;
using GARP.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GARP.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IAutoRepository, AutoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
