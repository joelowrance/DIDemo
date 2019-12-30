using System;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Authentication.ExtendedProtection;
using Castle.Core.Logging;
using DependencyServices;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IoCContainers
{
    class Program
    {
        static void Main(string[] args)
        {
            // basic example of Microsoft's container
            var basicMicrosoft = new MicrosoftDemo();
            basicMicrosoft.Run();
            
            // basic example of Lamar
            var basicLamar = new LamarDemo();
            basicLamar.Run();
            
            // basic example of Autofac
            var basicAutofac = new AutoFacDemo();
            basicAutofac.Run();
            
            //more advanced usages...
            
            // assembly scanning
            var scanning = new AssemblyScanning();
            scanning.Demo();
            
            // interceptors
            var cachingDemo = new CachingInterceptionDemo();
            cachingDemo.Run();
            
            var exceptionInterceptorDemo = new ExceptionInterceptorDemo();
            exceptionInterceptorDemo.Run();

            Console.ReadLine();
        }
    }
}