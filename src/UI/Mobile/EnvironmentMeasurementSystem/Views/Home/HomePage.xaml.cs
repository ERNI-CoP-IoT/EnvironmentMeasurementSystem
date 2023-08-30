using EnvironmentMeasurementSystem.Application.Interfaces.Logging;
using EnvironmentMeasurementSystem.ViewModels.Home;
using Microsoft.Maui.Maps;

namespace EnvironmentMeasurementSystem.Views.Home;

public partial class HomePage
{
    private readonly ILoggingService loggingService;
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    public HomePage(HomeViewModel homeViewModel, ILoggingService loggingService)
    {
        this.loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        InitializeComponent();
        BindingContext = homeViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = SetCurrentLocation();
    }

    public async Task SetCurrentLocation()
    {
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            var location = await Geolocation.Default.GetLastKnownLocationAsync() ??
                           await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
            {
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
        finally
        {
            _isCheckingLocation = false;
        }
    }

    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }
}

