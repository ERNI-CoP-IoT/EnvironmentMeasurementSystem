using EnvironmentMeasurementSystem.ViewModels.AppShell;
using EnvironmentMeasurementSystem.Views.Alerts;

namespace EnvironmentMeasurementSystem.Views.ApplicationShell;

public partial class AppShell : Shell
{
    public AppShell(AppShellViewModel appShellViewModel)
    {
        Routing.RegisterRoute(nameof(AlertDetailPage), typeof(AlertDetailPage));
        InitializeComponent();
        this.BindingContext = appShellViewModel;
    }
}
