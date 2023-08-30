using EnvironmentMeasurementSystem.Domain.Sensors;

namespace EnvironmentMeasurementSystem.Domain.Alerts;
public class Alert
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public AlertType Type { get; set; }
    public Sensor Sensor { get; set; }
}
