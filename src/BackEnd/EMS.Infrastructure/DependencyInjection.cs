using EMS.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Infrastructure
{
    /// <summary>
    /// The dependency injection class.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ISensorsService, SensorsService>();
            services.AddTransient<IAlertsService, AlertsService>();
            return services;
        }
    }
}
