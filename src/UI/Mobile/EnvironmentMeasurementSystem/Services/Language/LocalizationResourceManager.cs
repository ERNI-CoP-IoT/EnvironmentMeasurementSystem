namespace EnvironmentMeasurementSystem.Services.Language;

internal class LocalizationResourceManager : INotifyPropertyChanged
{
    private LocalizationResourceManager()
    {
        AppResources.Culture = CultureInfo.CurrentCulture;
    }

    public static LocalizationResourceManager Instance { get; } = new();

    public object this[string resourceKey]
        => AppResources.ResourceManager.GetObject(resourceKey, AppResources.Culture) ?? string.Empty;

    public event PropertyChangedEventHandler PropertyChanged;

    public void SetCulture(CultureInfo culture)
    {
        AppResources.Culture = culture;
        Preferences.Set("Language", culture.Name);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }
}
