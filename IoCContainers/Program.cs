using System;
using System.Runtime.InteropServices.ComTypes;
using DependencyServices;

namespace IoCContainers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var instance = new OpenWeatherMapService();
            var result = instance.GetWeatherForLocation(21144);
        }
    }
}