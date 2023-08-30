using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;

namespace EnvironmentMeasurementSystem.Services.Authentication;
public class AuthenticationService : IAuthenticationService
{
    private readonly IConfigurationSection configuration;
    private readonly IPublicClientApplication authenticationClient;

    public AuthenticationResult AuthenticationResult { get; internal set; }

    public AuthenticationService(IPublicClientApplicationBuilder publicClientApplicationBuilder, IConfiguration config)
    {
        authenticationClient = publicClientApplicationBuilder.Create();
        configuration = config.GetSection("AzureAdB2C");
    }

    public async Task<AuthenticationResult> AcquireTokenSilent(CancellationToken cancellationToken)
    {
        try
        {
            var accounts = await authenticationClient.GetAccountsAsync(configuration.GetValue<string>("SignInPolicy"));
            var firstAccount = accounts.FirstOrDefault();
            if (firstAccount is null)
            {
                return null;
            }
            AuthenticationResult= await authenticationClient.AcquireTokenSilent(GetScopes(), firstAccount)
                                             .ExecuteAsync(cancellationToken);
            return AuthenticationResult;
        }
        catch (MsalUiRequiredException)
        {
            return null;
        }
    }

    public async Task LogoutAsync(CancellationToken cancellationToken)
    {
        var accounts = await authenticationClient.GetAccountsAsync();
        foreach (var account in accounts)
        {
            await authenticationClient.RemoveAsync(account);
        }
    }

    public async Task<AuthenticationResult> SignInInteractively(CancellationToken cancellationToken)
    {
        AuthenticationResult= await authenticationClient.AcquireTokenInteractive(GetScopes()).WithPrompt(Prompt.ForceLogin)
#if WINDOWS
                                         .WithUseEmbeddedWebView(false)
#endif
                                         .ExecuteAsync(cancellationToken);
        return AuthenticationResult;
    }

    private IEnumerable<string> GetScopes()
    {
        var response = new List<string>();
        var scopes = configuration.GetSection("Scopes");
        if (scopes != null)
        {
            var scopeCollection = scopes.GetChildren();
            if (scopeCollection != null)
            {
                foreach (var section in scopeCollection)
                {
                    response.Add(section.Value);
                }
            }
        }
        return response;
    }
}

