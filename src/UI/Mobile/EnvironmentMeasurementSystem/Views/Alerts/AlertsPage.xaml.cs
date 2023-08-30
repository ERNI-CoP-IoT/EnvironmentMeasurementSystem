using EnvironmentMeasurementSystem.ViewModels.Alerts;
using Microsoft.Maui.Maps;

namespace EnvironmentMeasurementSystem.Views.Alerts;

public partial class AlertsPage
{
    public AlertsPage(AlertsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
