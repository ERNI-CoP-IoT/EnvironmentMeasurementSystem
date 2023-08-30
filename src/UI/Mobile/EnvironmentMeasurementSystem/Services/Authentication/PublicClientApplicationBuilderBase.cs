namespace EnvironmentMeasurementSystem.Services.Authentication;

public abstract class PublicClientApplicationBuilderBase
{
    internal readonly IConfigurationSection configuration;
    public readonly string ClientId;
    public readonly string SignInPolicy;
    public readonly string TenantName;
    public readonly string TenantId;
    public readonly string AuthorityBase;
    public readonly string AuthoritySignIn;

    protected PublicClientApplicationBuilderBase(IConfiguration config)
    {
        configuration = config.GetSection("AzureAdB2C");
        ClientId = configuration.GetValue<string>("ClientId");
        SignInPolicy = configuration.GetValue<string>("SignUpSignInPolicyId");
        TenantName = configuration.GetValue<string>("TenantName");
        TenantId = configuration.GetValue<string>("TenantId");
        AuthorityBase = $"https://{TenantName}.b2clogin.com/tfp/{TenantId}/";
        AuthoritySignIn = $"{AuthorityBase}{SignInPolicy}";
    }

    internal PublicClientApplicationBuilder CreatePublicClientApplicationBuilder()
    {
        return PublicClientApplicationBuilder.Create(ClientId)
                                             .WithB2CAuthority(AuthoritySignIn);
    }
}

#if ANDROID
public class PublicClientApplicationBuilderAndroid : PublicClientApplicationBuilderBase, IPublicClientApplicationBuilder
{

    public PublicClientApplicationBuilderAndroid(IConfiguration config) : base(config)
    {
    }

    public IPublicClientApplication Create()
    {
        return CreatePublicClientApplicationBuilder()
                .WithParentActivityOrWindow(() => Platform.CurrentActivity)
                .WithRedirectUri($"msal{ClientId}://auth")
                .Build();
    }
}
#endif

#if IOS || MACCATALYST
public class PublicClientApplicationBuilderApple : PublicClientApplicationBuilderBase, IPublicClientApplicationBuilder
{
    public PublicClientApplicationBuilderApple(IConfiguration config) : base(config)
    {

    }
    public IPublicClientApplication Create()
    {
        return CreatePublicClientApplicationBuilder()
               .WithRedirectUri($"msal{ClientId}://auth")
               .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
               .Build();
    }
}
#endif

#if WINDOWS
public class PublicClientApplicationBuilderWindows : PublicClientApplicationBuilderBase, IPublicClientApplicationBuilder
{

    public PublicClientApplicationBuilderWindows(IConfiguration config):base(config)
    {
    }

    public IPublicClientApplication Create()
    {
        return CreatePublicClientApplicationBuilder()
               .WithRedirectUri("http://localhost")
               .Build();
    }
}
#endif
