using Microsoft.Extensions.Hosting;
using EMS.Infrastructure;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((services) =>
    {
        services.AddServices();
    })
    .Build();

host.Run();
