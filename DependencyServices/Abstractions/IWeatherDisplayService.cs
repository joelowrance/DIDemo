using DependencyServices.WeatherModels;

namespace DependencyServices.Abstractions
{
    public interface IWeatherDisplayService
    {
        string GenerateDisplay(WeatherResult weatherResult, string shirtType);
    }
}