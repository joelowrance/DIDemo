using System;
using System.Net.Http;
using DepenencyModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DependencyServices
{

    public interface IDemoService
    {
        string WhatShouldIWear(int zipCode);
    }

    public class DemoService : IDemoService
    {
        private readonly IWeatherService _weatherService;

        public DemoService(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public string WhatShouldIWear(int zipCode)
        {
            var temp = _weatherService.GetWeatherForLocation(21144)?.main?.temp;

            if (temp < 40)
            {
                return "Jacket";
            }
            
            if (temp < 50)
            {
                return "Sweatshirt";
            }
            
            return "TShirt";
        }
    }
    
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