using LanguageDto = EnvironmentMeasurementSystem.Domain.Languages.Language;
namespace EnvironmentMeasurementSystem.Services.Language;

internal class LanguageService : ILanguageService
{
    private readonly List<LanguageDto> availableLanguages = new List<LanguageDto>
    {
        new LanguageDto
        {
            Name = "English",
            Description = "US english",
            LanguageCode = "en-US"
        },
        new LanguageDto
        {
            Name = "Spanish",
            Description = "Spanish (spain)",
            LanguageCode = "es-ES"
        }
    };
    public CultureInfo GetCulture()
    {
        return new CultureInfo(Preferences.Get("Language", "en-US"));
    }

    public void SetCulture(CultureInfo culture)
    {
        LocalizationResourceManager.Instance.SetCulture(culture);
    }

    public List<LanguageDto> GetAvailableLanguages()
    {
        return availableLanguages;
    }
}