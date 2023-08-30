using EnvironmentMeasurementSystem.Services.Language;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EnvironmentMeasurementSystem;
public static class MauiProgram
{
    private static IConfigurationRoot configurationRoot;
    public static MauiApp CreateMauiApp()
    {
        InitializeConfiguration();
        InitializeApplicationLanguage();
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .AddEnvironmentMeasurementSystem(configurationRoot);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static void InitializeApplicationLanguage()
    {
        var culture = Thread.CurrentThread.CurrentCulture;
        if (Preferences.ContainsKey("Language"))
        {
            culture = new CultureInfo(Preferences.Get("Language", "en-US"));
        }

        LocalizationResourceManager.Instance.SetCulture(culture);
    }

    private static void InitializeConfiguration()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream($"{typeof(MauiProgram).Namespace}.appsettings.json");
        configurationRoot = new ConfigurationBuilder().AddJsonStream(stream).Build();
    }
}
