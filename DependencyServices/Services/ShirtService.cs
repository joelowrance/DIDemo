using System;
using DependencyServices.Abstractions;
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
                var weather = _weatherService.GetWeatherForLocation(21144);
                var message = _displayService.GenerateDisplay(weather);
                _logger.LogInformation($"Got weather:  {message}");
                return message;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unable to get weather.");
                return "Unknown.  Look out the window.";
            }
        }
    }
}