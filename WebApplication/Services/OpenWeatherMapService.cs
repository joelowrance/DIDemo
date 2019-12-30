using System.Net.Http;
using DependencyServices;
using DependencyServices.Abstractions;
using DependencyServices.WeatherModels;
using Newtonsoft.Json;

namespace WebApplication.Services
{
    public class OpenWeatherMapService : IWeatherService
    {
        public WeatherResult GetWeatherForLocation(int zipCode)
        {
            var url = $"https://samples.openweathermap.org/data/2.5/weather?zip={zipCode}&appid=d56e5ac459c38db397702745bfb21ddb";

            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<WeatherResult>(response);
            }
        }
    }
}