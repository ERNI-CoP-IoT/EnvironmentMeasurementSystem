using EnvironmentMeasurementSystem.ViewModels.LogIn;

namespace EnvironmentMeasurementSystem.Views.LogIn;

public partial class LogInPage : ContentPage
{
    public LogInPage(LogInViewModel logInViewModel)
    {
        InitializeComponent();
        BindingContext = logInViewModel;
    }
}
