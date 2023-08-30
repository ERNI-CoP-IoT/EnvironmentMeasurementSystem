using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentMeasurementSystem.Domain.Sensors;
public class Sensor
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public double Temperature { get; set; }
    public string TemperatureUnit { get; set; }
    public double Humidity{ get; set; }
    public string HumidityUnit { get; set; }
    public double Co2 { get; set; }
    public string Co2Unit { get; set; }
    public double NOx { get; set; }
    public string NOxUnit { get; set; }
    public double Pm25{ get; set; }
    public string Pm25Unit { get; set; }
    public double Pm10 { get; set; }
    public string Pm10Unit { get; set; }
    public double Pressure{ get; set; }
    public string PressureUnit { get; set; }
    public double BatteryLevel{ get; set; }
    public string BatteryLevelUnit { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }

}
