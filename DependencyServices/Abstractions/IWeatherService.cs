using DependencyServices.WeatherModels;

namespace DependencyServices.Abstractions
{
    public interface IWeatherService
    {
        WeatherResult GetWeatherForLocation(int zipCode);
    }
}