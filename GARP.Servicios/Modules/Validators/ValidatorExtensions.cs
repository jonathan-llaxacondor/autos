using GARP.Application.Validators;

namespace GARP.Servicios.Modules.Validators
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddTransient<DtoCUAutoValidator>();
            return services;
        }
    }
}
