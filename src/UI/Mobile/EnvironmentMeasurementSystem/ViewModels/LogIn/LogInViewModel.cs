using System.IdentityModel.Tokens.Jwt;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EnvironmentMeasurementSystem.Application.Interfaces.Logging;
using EnvironmentMeasurementSystem.Messages.Authentication;
using EnvironmentMeasurementSystem.Services.Authentication;
using EnvironmentMeasurementSystem.Views.Home;

namespace EnvironmentMeasurementSystem.ViewModels.LogIn;
public partial class LogInViewModel : BaseViewModel
{
    private readonly IAuthenticationService authenticationService;
    private readonly ILoggingService loggingService;

    public LogInViewModel(IAuthenticationService authenticationService, ILoggingService loggingService)
    {
        this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        this.loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
    }
    [RelayCommand]
    public async Task LogIn()
    {
        AuthenticationResult result = null;
        bool tryInteractiveLogin = false;

        try
        {
            var response = await authenticationService.SignInInteractively(CancellationToken.None);
            var token = response?.IdToken; // you can also get AccessToken if you need it
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken data = handler.ReadJwtToken(token);
                var claims = data.Claims.ToList();
                if (data != null)
                {
                    var textClaims = string.Join("\r\n", claims.Select(item => $"{item.Subject} {item.Value}"));
                    WeakReferenceMessenger.Default.Send(new LogInSuccessFullMessage(data));
                }

                if (MainThread.IsMainThread)
                {
                    await GoToHome();
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(async () => { await GoToHome(); });
                }
            }
        }
        catch (Exception ex)
        {
            loggingService.Error(ex);
        }
    }

    private async Task GoToHome()
    {
        await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
    }
}
