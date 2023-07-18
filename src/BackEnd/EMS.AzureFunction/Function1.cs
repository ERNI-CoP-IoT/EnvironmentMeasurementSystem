using System.Net;
using EMS.Infrastructure.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace EMS.AzureFunction
{
    public class Function1
    {
        private readonly ILogger logger;
        private readonly ISensorsService sensorsService;
        private readonly IAlertsService alertsService;

        public Function1(ILoggerFactory loggerFactory, ISensorsService sensorsService, IAlertsService alertsService)
        {
            this.logger = loggerFactory.CreateLogger<Function1>();
            this.sensorsService = sensorsService;
            this.alertsService = alertsService;
        }

        [Function(nameof(GetSensors))]
        public async Task<HttpResponseData> GetSensors([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            this.logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            var rep = this.sensorsService.GetSensors();
            await response.WriteAsJsonAsync(rep);

            return response;
        }

        [Function(nameof(GetMarkers))]
        public async Task<HttpResponseData> GetMarkers([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            this.logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            var rep = this.sensorsService.GetMarkers();
            await response.WriteAsJsonAsync(rep);

            return response;
        }

        [Function(nameof(GetAlerts))]
        public async Task<HttpResponseData> GetAlerts([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            this.logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            var rep = this.alertsService.GetAlerts();
            await response.WriteAsJsonAsync(rep);

            return response;
        }
    }
}
