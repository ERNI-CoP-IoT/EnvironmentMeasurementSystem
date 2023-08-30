using EnvironmentMeasurementSystem.Application.Interfaces.Alerts;
using EnvironmentMeasurementSystem.Application.Interfaces.Logging;
using EnvironmentMeasurementSystem.Domain.Alerts;
using EnvironmentMeasurementSystem.Domain.Sensors;

namespace EnvironmentMeasurementSystem.Infrastructure.Services.Alerts;
internal class AlertsService : IAlertsService
{
    private readonly List<Alert> alerts;
    private readonly ILoggingService loggingService;

    public AlertsService(ILoggingService loggingService)
    {
        this.loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));

        alerts = new Bogus.Faker<Alert>()
                             .RuleFor(a => a.Id, f => f.Random.Guid())
                             .RuleFor(a => a.Name, f => f.Random.AlphaNumeric(5))
                             .RuleFor(a => a.Date, f => f.Date.Past())
                             .RuleFor(a => a.Description, f => f.Lorem.Text())
                             .RuleFor(a => a.Type, f => f.Random.Enum<AlertType>())
                             .RuleFor(a => a.Sensor, f => new Sensor { Id = f.Random.Guid(), Longitude = f.Random.Int(-180, 180), Latitude = f.Random.Int(-90, 90), Name = f.Random.AlphaNumeric(5) })
                             .Generate(50);
    }

    public Task<Alert> GetAlert(string alertId)
    {
        return Task.FromResult(alerts.Find(item => item.Id.ToString() == alertId));
    }

    public Task<List<Alert>> GetAlerts(AlertType? alertType)
    {
        return Task.FromResult(alerts.Where(item => ((alertType == null && !alertType.HasValue) || (alertType.HasValue && item.Type == alertType.Value))).ToList());
    }

    public Task<List<Alert>> GetAlerts(AlertType? alertType, DateTime dateFrom, DateTime dateTill)
    {
        try
        {

            return Task.FromResult(alerts.Where(item => ((alertType == null && !alertType.HasValue) ||
                                                         (alertType.HasValue && item.Type == alertType.Value)) &&
                                                         item.Date >= dateFrom &&
                                                         item.Date <= dateTill).ToList());
        }
        catch (Exception ex)
        {
            loggingService.Error(ex);
            return Task.FromResult(new List<Alert>());
        }
    }

    public Task<List<Alert>> GetLatestAlerts()
    {
        return Task.FromResult(alerts.OrderByDescending(item => item.Date).Take(10).ToList());
    }
}
