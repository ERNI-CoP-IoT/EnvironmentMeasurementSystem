using EnvironmentMeasurementSystem.Services.Language;

namespace EnvironmentMeasurementSystem.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    bool isBusy = false;

    public virtual async Task InitializeAsync()
    {
        if (!App.HasConnectivity())
        {
            await App.Current.MainPage.DisplayAlert(LocalizationResourceManager.Instance["System_Error"].ToString(),
                                                    LocalizationResourceManager.Instance["Snack_Message_NoInternetConnection"].ToString(),
                                                    LocalizationResourceManager.Instance["System_Ok"].ToString());
        }
    }

    public virtual Task UninitializedAsync() => Task.CompletedTask;
}
