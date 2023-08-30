namespace EnvironmentMeasurementSystem.Application.Interfaces.HttpsClientHandler;
public interface IHttpsClientHandlerService : IService
{
    HttpMessageHandler GetPlatformMessageHandler();
}
