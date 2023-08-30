using Microsoft.Maui.Controls.Maps;

namespace EnvironmentMeasurementSystem.ViewModels.Alerts;

public partial class AlertDetailViewModel : BaseViewModel
{
    private readonly IAlertsService alertsService;

    [ObservableProperty]
    string alertId;

    [ObservableProperty]
    Alert alert;

    [ObservableProperty]
    ObservableCollection<SensorModel> sensors;

    public AlertDetailViewModel(IAlertsService alertsService)
    {
        this.alertsService = alertsService ?? throw new ArgumentNullException(nameof(alertsService));
        Sensors = new ObservableCollection<SensorModel>();
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await GetAlert();
    }

    private async Task GetAlert()
    {
        Alert = await alertsService.GetAlert(AlertId);
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            Sensors.Clear();
            if (Alert != null)
            {
                Sensors.Add(new SensorModel(Alert.Sensor));
            }
        });
    }
}
