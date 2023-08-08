using EMS.Domain.Dtos;

namespace EMS.Infrastructure.Services;

public interface ISensorsService
{
    List<Sensor> GetSensors();

    List<Marker> GetMarkers();
}