using EnvironmentMeasurementSystem.Application.Interfaces.Alerts;
using EnvironmentMeasurementSystem.Infrastructure.Services.Alerts;

namespace EnvironmentMeasurementSystem.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ISensorsService, SensorsService>();
        services.AddSingleton<IAlertsService, AlertsService>();
        return services;
    }
}
