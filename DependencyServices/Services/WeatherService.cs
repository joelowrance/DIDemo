using DependencyServices.Abstractions;
using DependencyServices.WeatherModels;

namespace DependencyServices.Services
{
    public class WeatherService : IWeatherService
    {
        public WeatherResult GetWeatherForLocation(int zipCode)
        {
            return new WeatherResult
            {
                main = new Main
                {
                    humidity = 25,
                    pressure = 34,
                    temp = 54,
                    temp_max = 54,
                    temp_min = 25
                }
            };
        }
    }
}