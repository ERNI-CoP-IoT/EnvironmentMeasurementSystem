using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Maps;
using EnvironmentMeasurementSystem.Views.Alerts;
using EnvironmentMeasurementSystem.Views.Home;
using EnvironmentMeasurementSystem.Views.LogIn;
using EnvironmentMeasurementSystem.Views.Profile;
using EnvironmentMeasurementSystem.Views.Settings;
using EnvironmentMeasurementSystem.Views.ApplicationShell;
using EnvironmentMeasurementSystem.ViewModels.Alerts;
using EnvironmentMeasurementSystem.ViewModels.Home;
using EnvironmentMeasurementSystem.ViewModels.LogIn;
using EnvironmentMeasurementSystem.ViewModels.Profile;
using EnvironmentMeasurementSystem.ViewModels.Settings;
using EnvironmentMeasurementSystem.ViewModels.AppShell;
using EnvironmentMeasurementSystem.Services.Authentication;
using EnvironmentMeasurementSystem.Application.Interfaces;
using EnvironmentMeasurementSystem.Application.Interfaces.Logging;

namespace EnvironmentMeasurementSystem;
internal static class DependencyInjection
{
    public static MauiAppBuilder AddEnvironmentMeasurementSystem(this MauiAppBuilder builder, IConfigurationRoot configuration)
    {
        builder
            .AddViewModels()
            .AddPages()
            .AddFonts()
            .AddConfiguration(configuration)
            .AddMauiCommunityToolKit(configuration)
            .AddServices();
        return builder;
    }

    private static MauiAppBuilder AddViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<AlertsViewModel>();
        builder.Services.AddTransient<AlertDetailViewModel>();
        builder.Services.AddTransient<AppShellViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<LogInViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        return builder;
    }

    private static MauiAppBuilder AddPages(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<AlertsPage>();
        builder.Services.AddTransient<AlertDetailPage>();
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<LogInPage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<SettingsPage>();
        return builder;
    }
    private static MauiAppBuilder AddFonts(this MauiAppBuilder builder)
    {
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("FontAwesome6FreeBrands.otf", "FontAwesomeBrands");
            fonts.AddFont("FontAwesome6FreeRegular.otf", "FontAwesomeRegular");
            fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("ABeeZee-Regular.ttf", "ABeeZee");
            fonts.AddFont("BrandonGrotesque-Light.ttf", "BrandonGrotesqueLight");
            fonts.AddFont("BrandonGrotesque-Medium.ttf", "BrandonGrotesqueMedium");
            fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");
            fonts.AddFont("Montserrat-Regular.ttf", "MontserratRegular");
            fonts.AddFont("Tumbly.otf", "Tumbly");
        });
        return builder;
    }

    private static MauiAppBuilder AddConfiguration(this MauiAppBuilder builder, IConfigurationRoot configuration)
    {
        builder.Configuration.AddConfiguration(configuration);
        return builder;
    }

    private static MauiAppBuilder AddMauiCommunityToolKit(this MauiAppBuilder builder, IConfigurationRoot configuration)
    {
        var bingMapsLicense = configuration.GetValue<string>("Licenses:BingMapsLicense");
        builder
#if DEBUG
            .UseMauiCommunityToolkit()
#else
            .UseMauiCommunityToolkit(options =>
            {
                options.SetShouldSuppressExceptionsInConverters(true);
                options.SetShouldSuppressExceptionsInBehaviors(true);
                options.SetShouldSuppressExceptionsInAnimations(true);
            })
#endif
            .UseMauiCommunityToolkitMaps(bingMapsLicense);
        return builder;
    }

    private static MauiAppBuilder AddServices(this MauiAppBuilder builder)
    {
        builder.Services.AddInfrastructure();
        builder.Services.AddServices();
        return builder;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        var classes = types.Where(t => t.IsClass && t.GetInterfaces().Any(i => i.GetInterfaces().Any(ii => ii.FullName == typeof(IService).FullName))).ToList();
        foreach (var serviceClass in classes)
        {
            var interfaceService = serviceClass.GetInterfaces().FirstOrDefault(i => i.FullName != typeof(IService).FullName);
            services.AddSingleton(interfaceService, serviceClass);
        }

#if ANDROID
        services.AddSingleton<IPublicClientApplicationBuilder, PublicClientApplicationBuilderAndroid>();
#elif WINDOWS
        services.AddSingleton<IPublicClientApplicationBuilder,PublicClientApplicationBuilderWindows>();
#else
        services.AddSingleton<IPublicClientApplicationBuilder, PublicClientApplicationBuilderApple>();
#endif
#if DEBUG
        services.AddSingleton<ILoggingService, DebugLoggingService>();
#else
        services.AddSingleton<ILoggingService, AppCenterLoggingService>();
#endif

        return services;
    }

}