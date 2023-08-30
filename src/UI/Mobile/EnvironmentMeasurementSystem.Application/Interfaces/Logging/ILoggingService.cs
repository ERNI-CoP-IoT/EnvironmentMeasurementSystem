using System.Text.Json;

namespace EnvironmentMeasurementSystem.Application.Interfaces.Logging;
public interface ILoggingService
{
    void TrackEvent<T>(string eventName, T message);

    void TrackEvent(string eventName, string message);

    void TrackEvent(string eventName);

    void Debug(string message);

    void Warning(string message);

    void Error(Exception exception);
}
