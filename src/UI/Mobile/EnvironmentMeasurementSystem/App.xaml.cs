using EnvironmentMeasurementSystem.Views.ApplicationShell;

namespace EnvironmentMeasurementSystem;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App(IServiceProvider services)
    {
        InitializeComponent();
        var appShell = services.GetRequiredService<AppShell>();
        MainPage = appShell;
    }


    internal static bool HasConnectivity()
    {
        return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
    }

    internal static void SetMainPage(Page page)
    {
        App.Current.MainPage = page;
    }
}