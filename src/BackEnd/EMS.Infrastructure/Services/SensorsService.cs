using EMS.Domain.Dtos;

namespace EMS.Infrastructure.Services;

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
                Id = Guid.NewGuid(),
                BatteryLevel = randomNumOfSensors.Next(100),
                BatteryLevelUnit = "%",
                Description = "This is the fake description",
                Humidity = randomNumOfSensors.Next(100),
                HumidityUnit = "%",
                Name = $"Name {i}",
                Temperature = randomNumOfSensors.Next(),
                TemperatureUnit = "ºC",
                Co2 = randomNumOfSensors.Next(50),
                Co2Unit = "ppm",
                NOx = randomNumOfSensors.Next(50),
                NOxUnit = "ppm",
                Pm25 = randomNumOfSensors.Next(100),
                Pm25Unit = "ug/m3",
                Pm10 = randomNumOfSensors.Next(100),
                Pm10Unit = "ug/m3",
                Pressure = randomNumOfSensors.Next(3000),
                PressureUnit = "Pa",
                Type = "Temperature",
                Longitude = Math.Round(-90 + randomNumOfSensors.NextDouble() * 180, 6),
                Latitude = Math.Round(-180 + randomNumOfSensors.NextDouble() * 360, 6)
            }); ;
        }
        return response;
    }
}