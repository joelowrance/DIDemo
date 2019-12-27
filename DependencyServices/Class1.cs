using System;
using System.Net.Http;
using DepenencyModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DependencyServices
{
    public interface IWeatherService
    {
        WeatherResult GetWeatherForLocation(int zipCode);
    }

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

    public class StubWeatherService : IWeatherService
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