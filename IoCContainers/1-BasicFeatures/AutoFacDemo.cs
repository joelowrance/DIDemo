using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DependencyServices.Abstractions;
using DependencyServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IoCContainers
{
    public class AutoFacDemo
    {
        public void Run()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<WeatherService>().As<IWeatherService>();
            containerBuilder.RegisterType<ShirtService>().As<ShirtService>();
            containerBuilder.RegisterType<WeatherDisplayService>().As<IWeatherDisplayService>();
            
            //Add logging
            var services = new ServiceCollection();
            services.AddLogging();
            containerBuilder.Populate(services);
            
            var container = containerBuilder.Build();
            var demoService = container.Resolve<ShirtService>();
            var result = demoService.WhatShouldIWear(21144);
            
            Console.WriteLine(result);
        }
    }
}