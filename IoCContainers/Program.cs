using System;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Authentication.ExtendedProtection;
using Autofac;
using DependencyServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IoCContainers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //var instance = new OpenWeatherMapService();
            //var result = instance.GetWeatherForLocation(21144);
            //var demo = new AutoFacDemo();
            //demo.Run();
            
            var d2 = new LamarDemo();
            d2.Run();

            //var d3 = new MicrosoftDemo();
            //d3.Run();
            
        }
    }

    public class MicrosoftDemo
    {
        public void Run()
        {
            var provider = new Microsoft.Extensions.DependencyInjection.ServiceCollection()
                .AddSingleton<IWeatherService, WeatherService>()
                .AddSingleton<DemoService, DemoService>()
                .BuildServiceProvider();

            
            var demoService =provider.GetRequiredService<DemoService>();
            var temp = demoService.WhatShouldIWear(21144);
        }
    }
    
    public class LamarDemo
    {
        public void Run()
        {
            var container = new Lamar.Container(x => { 
                x.For<IWeatherService>().Use<WeatherService>();
                x.For<DemoService>().Use<DemoService>();
            });

            var container2 = new Lamar.Container(_ =>
            {
                _.Scan(_scan =>
                {
                    _scan.Assembly(typeof(IDemoService).Assembly);
                    _scan.WithDefaultConventions();
                });
            });

            var demo = container.GetInstance<DemoService>();
            var temp = demo.WhatShouldIWear(21144);

            var demo2 = container2.GetInstance<DemoService>();
            var temp2 = demo2.WhatShouldIWear(21144);
        }
    }
    
    public class AutoFacDemo
    {
        public void Run()
        {
            var containerBuilder = new Autofac.ContainerBuilder();
            containerBuilder.RegisterType<WeatherService>().As<IWeatherService>();
            containerBuilder.RegisterType<DemoService>().As<DemoService>();
            var container = containerBuilder.Build();

            var demoService = container.Resolve<DemoService>();
            var temp = demoService.WhatShouldIWear(21144);
        }
    }
}