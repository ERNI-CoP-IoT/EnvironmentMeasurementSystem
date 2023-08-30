

using EnvironmentMeasurementSystem.Application.Interfaces.Logging;

namespace EnvironmentMeasurementSystem.ViewModels.Alerts;
public partial class AlertsViewModel : BaseViewModel
{
    private readonly IAlertsService alertsService;
    private readonly ILoggingService loggingService;

    [ObservableProperty]
    DateTime? startDate;

    [ObservableProperty]
    DateTime? endDate;

    [ObservableProperty]
    AlertType typeOfAlert;

    [ObservableProperty]
    ObservableCollection<Alert> alerts;

    [ObservableProperty]
    Alert selectedAlert;

    [ObservableProperty]
    int criticalAlerts;

    [ObservableProperty]
    int errorAlerts;

    [ObservableProperty]
    int warningAlerts;

    [ObservableProperty]
    int infoAlerts;

    public IReadOnlyList<string> AllAlertTypes { get; } = Enum.GetNames(typeof(AlertType));

    public AlertsViewModel(IAlertsService alertsService, ILoggingService loggingService)
    {
        this.alertsService = alertsService ?? throw new ArgumentNullException(nameof(alertsService));
        this.loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        Alerts = new ObservableCollection<Alert>();
        StartDate = DateTime.Today.AddDays(-1);
        EndDate = DateTime.Today;
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await GetLatestAlerts();
    }

    [RelayCommand]
    public async Task GetLatestAlerts()
    {
        Alerts.Clear();
        try
        {
            IsBusy = true;
            var response = await alertsService.GetLatestAlerts();
            if (response != null)
            {
                AddAlerts(response);
            }
        }
        catch (Exception ex)
        {
            loggingService.Error(ex);
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async Task GetAlerts()
    {
        IsBusy = true;
        try
        {
            if (EndDate == null && !EndDate.HasValue)
            {
                EndDate = DateTime.Now;
            }

            if (StartDate == null && !StartDate.HasValue)
            {
                StartDate = DateTime.Now.AddMonths(-1);
            }

            var response = await alertsService.GetAlerts(TypeOfAlert, StartDate.Value, EndDate.Value);
            if (response != null)
            {
                AddAlerts(response);
            }
        }
        catch (Exception ex)
        {
            loggingService.Error(ex);
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async Task Selected(Alert alert)
    {
        if (alert == null)
            return;

        var route = $"{nameof(AlertDetailPage)}?AlertId={alert.Id}";
        await Shell.Current.GoToAsync(route);

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
