using System;
using DependencyServices.Abstractions;
using DependencyServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IoCContainers
{
    public class MicrosoftDemo
    {
        public void Run()
        {
            var provider = new Microsoft.Extensions.DependencyInjection.ServiceCollection()
                .AddSingleton<IWeatherService, WeatherService>()
                .AddSingleton<ShirtService, ShirtService>()
                .AddSingleton<IWeatherDisplayService, WeatherDisplayService>()
                .AddLogging() 
                .BuildServiceProvider();
            
            var demoService =provider.GetRequiredService<ShirtService>();
            var result  = demoService.WhatShouldIWear(21144);
            Console.WriteLine(result);
        }
    }
}