using System;
using System.Runtime.Caching;
using Castle.DynamicProxy;
using DependencyServices;
using DependencyServices.Abstractions;
using DependencyServices.WeatherModels;
using Microsoft.Extensions.Logging;

namespace IoCContainers
{
    public class FailingWeatherService : IWeatherService
    {
        public WeatherResult GetWeatherForLocation(int zipCode)
        {
            throw new Exception("Can't connect to the internet");
        }
    }
}