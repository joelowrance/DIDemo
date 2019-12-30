using System;
using DependencyServices.Abstractions;
using DependencyServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IoCContainers
{
    public class LamarDemo
    {
        public void Run()
        {
            var container = new Lamar.Container(x => {
                x.AddLogging();
                x.For<IWeatherService>().Use<WeatherService>();
                x.For<IWeatherDisplayService>().Use<WeatherDisplayService>();
                x.For<ShirtService>().Use<ShirtService>();
            });

            var demo = container.GetInstance<ShirtService>();
            var result = demo.WhatShouldIWear(21144);
            Console.WriteLine(result);
        }
    }
}