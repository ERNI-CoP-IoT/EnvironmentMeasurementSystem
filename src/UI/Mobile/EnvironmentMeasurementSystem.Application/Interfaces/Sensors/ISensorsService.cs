using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvironmentMeasurementSystem.Domain.Sensors;

namespace EnvironmentMeasurementSystem.Application.Interfaces.Sensors;
public interface ISensorsService
{
    Task<List<Sensor>> GetData();
    Task<List<Sensor>> GetMySensors();
}
