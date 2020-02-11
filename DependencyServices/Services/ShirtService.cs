using System;
using System.Buffers;
using DependencyServices.Abstractions;
using DependencyServices.WeatherModels;
using Microsoft.Extensions.Logging;

namespace DependencyServices.Services
{
    public class ShirtService : IShirtService
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger _logger;
        private readonly IWeatherDisplayService _displayService;

        public ShirtService(IWeatherService weatherService, ILogger<ShirtService> logger, IWeatherDisplayService displayService)
        {
            _weatherService = weatherService;
            _logger = logger;
            _displayService = displayService;
        }

        public string WhatShouldIWear(int zipCode)
        {
            try
            {
                var weather = _weatherService.GetWeatherForLocation(zipCode);
                var message = _displayService.GenerateDisplay(weather, CalculateShirt(weather));
                _logger.LogInformation($"Got weather:  {message}");
                return message;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unable to get weather.");
                return "Unknown.  Look out the window.";
            }
        }

        private string CalculateShirt(WeatherResult weatherResult)
        {
            if (weatherResult.main.temp < 40)
            {
                return "a jacket";
            }

            if (weatherResult.main.temp < 60)
            {
                return "a sweatshirt";
            }

            return "a t-shirt";
        }
    }
}