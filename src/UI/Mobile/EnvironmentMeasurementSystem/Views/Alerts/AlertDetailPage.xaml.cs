using EnvironmentMeasurementSystem.Application.Interfaces.Logging;
using EnvironmentMeasurementSystem.ViewModels.Alerts;
using Microsoft.Maui.Maps;

namespace EnvironmentMeasurementSystem.Views.Alerts;

[QueryProperty(nameof(AlertId), nameof(AlertId))]
public partial class AlertDetailPage
{
    private readonly AlertDetailViewModel alertDetailViewModel;
    private readonly ILoggingService loggingService;

    public string AlertId { get; set; }
    public AlertDetailPage(AlertDetailViewModel alertDetailViewModel, ILoggingService loggingService)
    {
        InitializeComponent();
        BindingContext = this.alertDetailViewModel = alertDetailViewModel;
        this.loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
    }

    protected override async void OnAppearing()
    {
        alertDetailViewModel.AlertId = AlertId;
        await alertDetailViewModel.InitializeAsync();
        SetCurrentLocation();
    }

    public void SetCurrentLocation()
    {
        CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        try
        {
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            if (alertDetailViewModel?.Alert.Sensor != null)
            {
                var location = new Location(alertDetailViewModel.Alert.Sensor.Latitude, alertDetailViewModel.Alert.Sensor.Longitude);
                MapSpan mapSpan = MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(0.444)).WithZoom(0.0005);
                TheMap.MoveToRegion(mapSpan);
            }
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex)
        {
            // Unable to get location
            loggingService.Error(ex);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
