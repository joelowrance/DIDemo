using System;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Authentication.ExtendedProtection;
using Autofac;
using DependencyServices;
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
            var demo = new AutoFacDemo();
            demo.Run();
        }
    }

    public class AutoFacDemo
    {
        public void Run()
        {
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var containerBuilder = new Autofac.ContainerBuilder();
            containerBuilder.RegisterType<StubWeatherService>().As<IWeatherService>();
            containerBuilder.RegisterType<DemoService>().As<DemoService>();
            var container = containerBuilder.Build();

            var demoService = container.Resolve<DemoService>();
            var temp = demoService.GetTemperature(21144);
        }
    }
}