namespace EMS.Domain.Dtos
{
    public class Sensor
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string CO2 { get; set; }
        public string NOx { get; set; }
        public string Pm25 { get; set; }
        public string Pm10 { get; set; }
        public string Pressure { get; set; }
        public string BatteryLevel { get; set; }
    }
}