using System;
using System.Runtime.Caching;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using DependencyServices.Abstractions;
using DependencyServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IoCContainers
{
    public class CachingInterceptionDemo
    {
        public void Run()
        {
            var containerBuilder = new Autofac.ContainerBuilder();
            containerBuilder.Register(c => new CachedInterceptor(System.Runtime.Caching.MemoryCache.Default));
            containerBuilder.RegisterType<WeatherDisplayService>().As<IWeatherDisplayService>();
            containerBuilder.RegisterType<WeatherService>().As<IWeatherService>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CachedInterceptor));
            
            //Add logging
            var services = new ServiceCollection();
            services.AddLogging();
            containerBuilder.Populate(services);
            
            var container = containerBuilder.Build();
            var demoService = container.Resolve<IWeatherService>();
            var temp = demoService.GetWeatherForLocation(21144);
            var temp2 = demoService.GetWeatherForLocation(21144);
        }
    }
    
    public class CachedInterceptor : IInterceptor
    {
        private System.Runtime.Caching.MemoryCache _cache;

        public CachedInterceptor(MemoryCache cache)
        {
            _cache = cache;
        }

        public void Intercept(IInvocation invocation)
        {
            var args = invocation.Arguments[0] as string;
            var key = "weather_for_" + args;

            if (_cache.Contains(key))
            {
                invocation.ReturnValue = _cache[key];
                return;
            }
            
            invocation.Proceed();

            if (invocation.ReturnValue != null)
            {
                _cache.Add(new  System.Runtime.Caching.CacheItem(key, invocation.ReturnValue),
                    new CacheItemPolicy {AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)});
            }
        }
    }
}