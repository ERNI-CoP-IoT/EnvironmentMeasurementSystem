using EMS.Domain.Dtos;

namespace EMS.Infrastructure.Services
{
    public class SensorsService : ISensorsService
    {
        public List<Marker> GetMarkers()
        {
            var randomNumOfMarkers = new Random();
            int num = randomNumOfMarkers.Next(50);

            var response = new List<Marker>();

            for (int i = 0; i < num; i++)
            {
                response.Add(new Marker()
                {
                    PosX = $"{randomNumOfMarkers.Next(50)}",
                    PosY = $"{randomNumOfMarkers.Next(50)}",
                    Message = $"Test marker {i}",
                });
            }
            return response;
        }

        public List<Sensor> GetSensors()
        {
            var randomNumOfSensors = new Random();
            int num = randomNumOfSensors.Next(50);

            var response = new List<Sensor>();

            for (int i = 0; i < num; i++)
            {
                response.Add(new Sensor()
                {
                    BatteryLevel = $"{randomNumOfSensors.Next(100)}%",
                    Description = "This is the fake description",
                    Humidity = $"{randomNumOfSensors.Next(100)}%",
                    Name = $"Name {i}",
                    Temperature = $"{randomNumOfSensors.Next()}ºC",
                    CO2 = $"{randomNumOfSensors.Next(50)} ppm",
                    NOx = $"{randomNumOfSensors.Next(50)} ppm",
                    Pm25 = $"{randomNumOfSensors.Next(100)} ug/m3",
                    Pm10 = $"{randomNumOfSensors.Next(100)} ug/m3",
                    Pressure = $"{randomNumOfSensors.Next(3000)} Pa",
                    Type = "Temperature"
                });
            }
            return response;
        }
    }
}