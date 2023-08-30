using EnvironmentMeasurementSystem.ViewModels.Profile;

namespace EnvironmentMeasurementSystem.Views.Profile;

public partial class ProfilePage
{
    public ProfilePage(ProfileViewModel profileViewModel)
    {
        InitializeComponent();
        BindingContext = profileViewModel;
    }
}
