using EnvironmentMeasurementSystem.Domain.Alerts;

namespace EnvironmentMeasurementSystem.Application.Interfaces.Alerts;
public interface IAlertsService
{
    Task<List<Alert>> GetLatestAlerts();
    Task<List<Alert>> GetAlerts(AlertType? alertType);
    Task<List<Alert>> GetAlerts(AlertType? alertType, DateTime dateFrom, DateTime dateTill);
    Task<Alert> GetAlert(string alertId);
}
