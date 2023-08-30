using Android.App;
using Android.Content;
using Android.Content.PM;

namespace EnvironmentMeasurementSystem.Platforms.Android;
[Activity(Theme = "@style/Maui.SplashTheme", 
          MainLauncher = true, 
          ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density,
          SupportsPictureInPicture = true,
          HardwareAccelerated = true,
          LaunchMode = LaunchMode.SingleInstance,
          Exported = true,
          ResizeableActivity = true,
          AllowEmbedded = true)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
    {
        base.OnActivityResult(requestCode, resultCode, data);
        AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
    }
}
