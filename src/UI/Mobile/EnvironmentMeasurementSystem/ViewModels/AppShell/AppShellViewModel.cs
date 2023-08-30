using CommunityToolkit.Mvvm.Messaging;
using EnvironmentMeasurementSystem.Messages.Authentication;
using EnvironmentMeasurementSystem.Services.Authentication;

namespace EnvironmentMeasurementSystem.ViewModels.AppShell;
public partial class AppShellViewModel : BaseViewModel, IRecipient<LogInSuccessFullMessage>
{
    private readonly IAuthenticationService authenticationService;
    [ObservableProperty]
    bool showLogIn;

    [ObservableProperty]
    bool showLogOut;

    [ObservableProperty]
    bool isFirstTime;

    [ObservableProperty]
    string userName;
    public AppShellViewModel(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
        WeakReferenceMessenger.Default.Register<LogInSuccessFullMessage>(this);
        UserName = "Please LogIn";

        if (VersionTracking.IsFirstLaunchEver)
        {
            IsFirstTime = true;
            return;
        }

        Refresh();
    }

    internal void Refresh()
    {
        ShowLogIn = true;
        ShowLogOut = !ShowLogIn;
    }

    public void Receive(LogInSuccessFullMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (message?.Value?.Claims?.Count() > 0)
            {
                var name = message?.Value?.Claims?.FirstOrDefault(item => item.Type == "name")?.Value ?? "";
                var givenName = message?.Value?.Claims?.FirstOrDefault(item => item.Type == "given_name")?.Value ?? "";
                var familyName = message?.Value?.Claims?.FirstOrDefault(item => item.Type == "family_name")?.Value ?? "";
                UserName = $"{name} {givenName} {familyName}";
                ShowLogIn = false;
                ShowLogOut = true;
                IsFirstTime = false;
            }
        });
    }
}
