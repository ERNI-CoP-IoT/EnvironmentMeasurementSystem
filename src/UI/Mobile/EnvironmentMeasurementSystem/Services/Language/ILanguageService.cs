using EnvironmentMeasurementSystem.Application.Interfaces;
using LanguageDto = EnvironmentMeasurementSystem.Domain.Languages.Language;
namespace EnvironmentMeasurementSystem.Services.Language;

public interface ILanguageService : IService
{
    List<LanguageDto> GetAvailableLanguages();
    void SetCulture(CultureInfo culture);
    CultureInfo GetCulture();
}
