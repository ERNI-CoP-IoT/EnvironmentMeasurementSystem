using CommunityToolkit.Mvvm.Input;
using EnvironmentMeasurementSystem.Application.Interfaces.Logging;
using EnvironmentMeasurementSystem.Application.Interfaces.Sensors;
using EnvironmentMeasurementSystem.Domain.Sensors;
using EnvironmentMeasurementSystem.Services.Authentication;
using EnvironmentMeasurementSystem.Services.Language;

namespace EnvironmentMeasurementSystem.ViewModels.Profile;
public partial class ProfileViewModel : BaseViewModel
{
    private readonly IAuthenticationService authenticationService;
    private readonly ISensorsService sensorsService;
    private readonly ILoggingService loggingService;

    [ObservableProperty]
    string userName;

    [ObservableProperty]
    string familyName;

    [ObservableProperty]
    string givenName;

    [ObservableProperty]
    string todayDate;

    [ObservableProperty]
    ObservableCollection<Sensor> mySensors;

    public ProfileViewModel(IAuthenticationService authenticationService, ISensorsService sensorsService, ILoggingService loggingService)
    {
        this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        this.sensorsService = sensorsService ?? throw new ArgumentNullException(nameof(sensorsService));
        this.loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));

        MySensors = new ObservableCollection<Sensor>();
        var month = LocalizationResourceManager.Instance[$"Month_{DateTime.Now.Month}"].ToString();
        if (!string.IsNullOrEmpty(month))
        {
            TodayDate = $"{DateTime.Now.Day} {month} {DateTime.Now.Year}";
        }
        else
        {
            TodayDate = DateTime.Now.ToString("dd MMMM yyyy");
        }

        this.loggingService = loggingService;
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await LoadData();
    }

    [RelayCommand]
    public async Task Refresh()
    {
        IsBusy = true;
        try
        {
            await LoadData();
        }
        catch (Exception ex)
        {
            loggingService.Error(ex);
        }
        IsBusy = false;
    }

    private async Task LoadData()
    {
        var response = authenticationService.AuthenticationResult;
        if (response != null)
        {
            var items = await sensorsService.GetMySensors();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                UserName = response.ClaimsPrincipal.Claims.FirstOrDefault(item => item.Type == "name")?.Value ?? "";
                GivenName = response.ClaimsPrincipal.Claims.FirstOrDefault(item => item.Type == "given_name")?.Value ?? "";
                FamilyName = response.ClaimsPrincipal.Claims.FirstOrDefault(item => item.Type == "family_name")?.Value ?? "";
                try
                {
                    if (MySensors == null)
                    {
                        MySensors = new ObservableCollection<Sensor>();
                    }
                    else
                    {
                        MySensors.Clear();
                    }
                    foreach (var sensor in items)
                    {
                        MySensors.Add(sensor);
                    }
                }
                catch (Exception ex)
                {
                    loggingService.Error(ex);
                }
            });
        }
    }
}
