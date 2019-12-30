using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using DependencyServices.Abstractions;
using DependencyServices.WeatherModels;

namespace IoCContainers
{
    public class ExceptionInterceptorDemo
    {
        public void Run()
        {
            var containerBuilder = new Autofac.ContainerBuilder();
            containerBuilder.Register(c => new ExceptionInterceptor());
            containerBuilder.RegisterType<FailingWeatherService>().As<IWeatherService>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(ExceptionInterceptor));
            

            var container = containerBuilder.Build();
            var demoService = container.Resolve<IWeatherService>();
            var result = demoService.GetWeatherForLocation(21144);
            Console.WriteLine(result);
        }
    }
    
    public class ExceptionInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                invocation.ReturnValue = new WeatherResult {main = new Main {temp_max = 100, temp = 50, temp_min = 0}};
            }
        }
    }
}