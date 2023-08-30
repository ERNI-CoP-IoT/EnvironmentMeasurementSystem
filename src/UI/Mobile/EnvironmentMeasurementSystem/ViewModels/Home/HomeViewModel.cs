using EnvironmentMeasurementSystem.Application.Interfaces.Logging;
using EnvironmentMeasurementSystem.Views.Alerts;

namespace EnvironmentMeasurementSystem.ViewModels.Home;
public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    ObservableCollection<SensorModel> sensors;

    [ObservableProperty]
    ObservableCollection<Alert> alerts;

    [ObservableProperty]
    int criticalAlerts;

    [ObservableProperty]
    int errorAlerts;

    [ObservableProperty]
    int warningAlerts;

    [ObservableProperty]
    int infoAlerts;

    private readonly ISensorsService sensorsService;
    private readonly IAlertsService alertsService;
    private readonly ILoggingService loggingService;

    public HomeViewModel(ISensorsService sensorsService, IAlertsService alertsService, ILoggingService loggingService)
    {
        this.sensorsService = sensorsService ?? throw new ArgumentNullException(nameof(sensorsService));
        this.alertsService = alertsService ?? throw new ArgumentNullException(nameof(alertsService));
        this.loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        Sensors = new ObservableCollection<SensorModel>();
        Alerts = new ObservableCollection<Alert>();
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await GetData();
    }

    [RelayCommand]
    public async Task GetData()
    {
        var items = await sensorsService.GetMySensors() ?? new List<Sensor>();
        var latestAlerts = await alertsService.GetLatestAlerts() ?? new List<Alert>();
        AddItemsToMap(items.Select(sensor => new SensorModel(sensor)).ToList());
        AddAlerts(latestAlerts);
    }

    [RelayCommand]
    public async Task ShowAllSensors()
    {
        await GetData();
    }

    [RelayCommand]
    public async Task ShowOnlyMySensors()
    {

        var items = await sensorsService?.GetMySensors();
        AddItemsToMap(items.Select(sensor => new SensorModel(sensor)).ToList());
    }

    [RelayCommand]
    public async Task SelectedAlert(Alert alert)
    {
        if (alert == null)
            return;

        var route = $"{nameof(AlertDetailPage)}?AlertId={alert.Id}";
        await Shell.Current.GoToAsync(route);
    }

    private void AddItemsToMap(List<SensorModel> items)
    {
        if (MainThread.IsMainThread)
        {
            try
            {
                Sensors.Clear();
                if (items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        Sensors.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                loggingService.Error(ex);
            }
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AddItemsToMap(items);
            });
        }

    }
    private void AddAlerts(List<Alert> items)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            CriticalAlerts = items.Count(item => item.Type == AlertType.Critical);
            ErrorAlerts = items.Count(item => item.Type == AlertType.Error);
            WarningAlerts = items.Count(item => item.Type == AlertType.Warning);
            InfoAlerts = items.Count(item => item.Type == AlertType.Info);
            Alerts.Clear();
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    Alerts.Add(item);
                }
            }
        });
    }
}
