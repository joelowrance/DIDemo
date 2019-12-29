using System;
using System.Runtime.Caching;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using DependencyServices;
using DepenencyModels;

namespace IoCContainers
{
    public class InterceptionDemo
    {
        public void RunExceptionDemo()
        {
            var containerBuilder = new Autofac.ContainerBuilder();
            containerBuilder.Register(c => new ExceptionInterceptor(new SimpleLoggerForInterceptionDemo()));
            containerBuilder.RegisterType<FailingWeatherService>().As<IWeatherService>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(ExceptionInterceptor));
            
            var container = containerBuilder.Build();
            var demoService = container.Resolve<IWeatherService>();
            var temp = demoService.GetWeatherForLocation(21144);
        }
        
        public void RunCachingDemo()
        {
            var containerBuilder = new Autofac.ContainerBuilder();
            containerBuilder.Register(c => new CachedInterceptor(System.Runtime.Caching.MemoryCache.Default));
            containerBuilder.RegisterType<ExpensiveWeatherService>().As<IWeatherService>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CachedInterceptor));
            
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
            var cachekey = "weather_for_" + args;

            if (_cache.Contains(cachekey))
            {
                invocation.ReturnValue = _cache[cachekey];
                return;
            }
            
            invocation.Proceed();

            if (invocation.ReturnValue != null)
            {
                _cache.Add(new  System.Runtime.Caching.CacheItem(cachekey, invocation.ReturnValue),
                    new CacheItemPolicy {AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)});
            }
        }
    }


    public class ExpensiveWeatherService : IWeatherService
    {
        public WeatherResult GetWeatherForLocation(int zipCode)
        {
            return new WeatherResult {main = new Main() {temp = 100}};
        }
    }

    public class ExceptionInterceptor : IInterceptor
    {
        private ILogger _logger;

        public ExceptionInterceptor(ILogger logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                _logger.WriteMessage(ex.Message);
            }
        }
    }

    public class FailingWeatherService : IWeatherService
    {
        public WeatherResult GetWeatherForLocation(int zipCode)
        {
            throw new Exception("Can't connect to the internet");
        }
    }
    
    
    public class SimpleLoggerForInterceptionDemo : ILogger
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}