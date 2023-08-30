using EnvironmentMeasurementSystem.Domain.Sensors;

namespace EnvironmentMeasurementSystem.Models;
public class SensorModel
{
    public Sensor Sensor { get; }
    public Location Location { get { return new Location(Sensor.Latitude, Sensor.Longitude); } }
    public SensorModel(Sensor sensor)
    { 
        Sensor = sensor;
    }
}
