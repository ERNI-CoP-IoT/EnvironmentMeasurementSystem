using EnvironmentMeasurementSystem.Application.Interfaces.Logging;

namespace EnvironmentMeasurementSystem.Services.Logging;

public class AppCenterLoggingService : ILoggingService
{
    public const string LogTag = "AppCenterEMS";
    private const string IS_SEND_LOG_DATA_ALLOWED = "IsSendLogDataAllowed";
    private readonly IConfigurationSection configuration;
    public AppCenterLoggingService(IConfiguration config)
    {
        configuration = config.GetSection("AppCenter");
        if (!AppCenter.Configured)
        {
            AppCenter.LogLevel = GetLogLevel(configuration["LogLevel"]);
            AppCenter.Start(
                $"ios={configuration["Keys:iOS"]}​;" +
                $"android={configuration["Keys:Android"]};" +
                $"macos={configuration["Keys:MacOS"]};" +
                $"windowsdesktop={configuration["Keys:WindowsDesktop"]}",
                typeof(Analytics),
                typeof(Crashes),
                typeof(Distribute));

            Crashes.HasCrashedInLastSessionAsync().ContinueWith(hasCrashed =>
            {
                AppCenterLog.Info(LogTag, "Crashes.HasCrashedInLastSession=" + hasCrashed.Result);
            });
            Crashes.GetLastSessionCrashReportAsync().ContinueWith(task =>
            {
                AppCenterLog.Info(LogTag, "Crashes.LastSessionCrashReport.StackTrace=" + task.Result?.StackTrace);
            });

        }
    }

    public void TrackEvent<T>(string eventName, T message)
    {
        if (Preferences.Get(IS_SEND_LOG_DATA_ALLOWED, false))
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            Analytics.TrackEvent(eventName, new Dictionary<string, string> { { "EventMessage", jsonMessage } });
        }
    }

    public void TrackEvent(string eventName, string message)
    {
        if (Preferences.Get(IS_SEND_LOG_DATA_ALLOWED, false))
        {
            Analytics.TrackEvent(eventName, new Dictionary<string, string> { { nameof(message), message } });
        }
    }

    public void TrackEvent(string eventName)
    {
        if (Preferences.Get(IS_SEND_LOG_DATA_ALLOWED, false))
        {
            Analytics.TrackEvent(eventName);
        }
    }

    public void Debug(string message)
    {
        if (Preferences.Get(IS_SEND_LOG_DATA_ALLOWED, false))
        {
            Analytics.TrackEvent(nameof(Debug), new Dictionary<string, string> { { nameof(message), message } });
        }
    }

    public void Warning(string message)
    {
        if (Preferences.Get(IS_SEND_LOG_DATA_ALLOWED, false))
        {
            Analytics.TrackEvent(nameof(Warning), new Dictionary<string, string> { { nameof(message), message } });
        }
    }

    public void Error(Exception exception)
    {
        if (Preferences.Get(IS_SEND_LOG_DATA_ALLOWED, false))
        {
            Crashes.TrackError(exception);
        }
    }

    private Microsoft.AppCenter.LogLevel GetLogLevel(string logLevel)
    {
        var response = Microsoft.AppCenter.LogLevel.None;
        switch (logLevel)
        {
            //
            // Summary:
            //     SDK emits all possible level of logs.
            case "Verbose":
                response = Microsoft.AppCenter.LogLevel.Verbose;
                break;
            //
            // Summary:
            //     SDK emits debug, info, warn, error and assert logs.
            case "Debug":
                response = Microsoft.AppCenter.LogLevel.Debug;
                break;
            //
            // Summary:
            //     SDK emits info, warn, error, and assert logs.
            case "Info":
                response = Microsoft.AppCenter.LogLevel.Info;
                break;
            //
            // Summary:
            //     SDK emits warn, error, and assert logs.
            case "Warn":
                response = Microsoft.AppCenter.LogLevel.Warn;
                break;
            //
            // Summary:
            //     SDK error and assert logs.
            case "Error":
                response = Microsoft.AppCenter.LogLevel.Error;
                break;
            //
            // Summary:
            //     Only assert logs are emitted by SDK.
            case "Assert":
                response = Microsoft.AppCenter.LogLevel.Assert;
                break;
            //
            // Summary:
            //     No log is emitted by SDK.
            case "None":
                response = Microsoft.AppCenter.LogLevel.None;
                break;
        }
        return response;
    }
}