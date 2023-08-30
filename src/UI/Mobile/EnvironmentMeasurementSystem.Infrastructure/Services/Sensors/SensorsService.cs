using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using EnvironmentMeasurementSystem.Application.Interfaces.HttpsClientHandler;
using EnvironmentMeasurementSystem.Application.Interfaces.Logging;
using EnvironmentMeasurementSystem.Domain.Sensors;

namespace EnvironmentMeasurementSystem.Infrastructure.Services.Sensors;
internal class SensorsService : ISensorsService
{
    private readonly IConfigurationSection configuration;
    private readonly ILoggingService loggingService;

    public SensorsService(IHttpsClientHandlerService service, IConfiguration config, ILoggingService loggingService )
    {
        configuration = config.GetSection("EMSSettings:ApiURLs");
        this.loggingService = loggingService?? throw new ArgumentNullException(nameof(loggingService));
    }

    public async Task<List<Sensor>> GetData()
    {
        return await Task.FromResult(new List<Sensor>());
    }

    public async Task<List<Sensor>> GetMySensors()
    {        
        var items = new List<Sensor>();
        try
        {    
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            items = await client.GetFromJsonAsync<List<Sensor>>($"{configuration["Sensors"]}GetSensors");
            client.Dispose();
        }
        catch (Exception ex)
        {
            loggingService.Error(ex);
        }
        return items;
    }
}
