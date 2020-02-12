using System;
using DependencyServices.Abstractions;
using DependencyServices.Services;
using DependencyServices.WeatherModels;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTestExamples
{
    public class UnitTestShirtService : IShirtService
    {
        private readonly IWeatherService _weatherServiceObject;
        private readonly FakeLogger _logger;
        private readonly DisplayServiceStub _formatter;

        public UnitTestShirtService(IWeatherService weatherServiceObject, FakeLogger logger,
            DisplayServiceStub formatter)
        {
            _weatherServiceObject = weatherServiceObject;
            _logger = logger;
            _formatter = formatter;
        }
        
        public string WhatShouldIWear(int zipCode)
        {
            try
            {
                var weather = _weatherServiceObject.GetWeatherForLocation(zipCode);
                var result = _formatter.GenerateDisplay(weather, CalculateShirt(weather));
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to get the weather");
                return "Unknown.  Look out the window";
            }
            return null;
        }
        
        private string CalculateShirt(WeatherResult weatherResult)
        {
            
            //note, just return sweater until the test for this
            
            if (weatherResult.main.temp < 40)
            {
                return "a jacket";
            }

             if (weatherResult.main.temp < 60)
             {
                 return "a sweatshirt";
             }
            
             return "a t-shirt";

            
        }
    }
    
    public class ShirtServiceTests
    {
        public WeatherResult _WeatherResult = new WeatherResult
        {
            main = new Main {temp = 40, temp_min = 20, temp_max = 55}
        };
        
        [Fact]
        public void WhatShouldIWear_Success_PassesTheZipCodeToTheWeatherService()
        {
            //Arrange
            var logger = new FakeLogger();
            var weatherService = new Mock<IWeatherService>();
            weatherService.Setup(x => x.GetWeatherForLocation(21144)).Returns(_WeatherResult);
            var shirtService= new UnitTestShirtService(weatherService.Object, logger, new DisplayServiceStub());
            
            //Act
            shirtService.WhatShouldIWear(21144);

            //Assert
            weatherService.VerifyAll();
        }
        

        [Fact]
        public void WhatShouldIWear_Success_PassesTheWeatherAndShirtTypeToTheFormatter()
        {
            //Arrange
            var logger = new FakeLogger();
            var weatherService = new Mock<IWeatherService>();
            var formatter = new DisplayServiceStub();
            weatherService.Setup(x => x.GetWeatherForLocation(It.IsAny<int>())).Returns(_WeatherResult);
            var shirtService= new UnitTestShirtService(weatherService.Object, logger, formatter);
            
            //Act
            shirtService.WhatShouldIWear(24455);

            //Assert
            //Assert.True(formatter.ShirtType == "a tshirt"); //Example where this is bad
            Assert.Equal("a sweatshirt", formatter.ShirtType); //Example where this is bad
        }

        
        [Fact]
        public void WhatShouldIWear_OnException_ReturnsADefaultValue()
        {
            //Arrange 
            var logger = new FakeLogger();
            var weatherService = new Mock<IWeatherService>();
            var formatter = new DisplayServiceStub();
            
            // --> force our fake weather service to simulate an exception
            weatherService.Setup(x => x.GetWeatherForLocation(It.IsAny<int>())).Throws(new NullReferenceException());
            
            var shirtService= new UnitTestShirtService(weatherService.Object, logger, formatter);
            
            //Act
            var result = shirtService.WhatShouldIWear(21144);

            //Assert
            Assert.NotNull(result);
            Assert.Contains("Unknown", result);
        }
        
        [Fact]
        public void WhatShouldIWear_Success_TellsMeTheRightShirt()
        {
            //Arrange 
            var logger = new FakeLogger();
            var weatherService = new WeatherServiceStubOrFakeOrWhatever();
            var formatter= new DisplayServiceStub();
            var service = new UnitTestShirtService(weatherService, logger, formatter);
            
            //Arrange, act, assert doesn't always fit (you can make multiple tests for this, but I don't see the point)
            weatherService.wr.main.temp = 39;
            var jacketResult = service.WhatShouldIWear(21144);
            Assert.Contains("jacket", jacketResult);
            
            weatherService.wr.main.temp = 59;
            var sweatshirtResult = service.WhatShouldIWear(21144);
            Assert.Contains("sweatshirt", sweatshirtResult);
            
            weatherService.wr.main.temp = 80;
            var tshirtResult  = service.WhatShouldIWear(21144);
            Assert.Contains("t-shirt", tshirtResult);
        }
    }



    public class WeatherServiceStubOrFakeOrWhatever : IWeatherService
    {
        public WeatherResult wr = new WeatherResult {main = new Main {temp = 50, temp_max = 60, temp_min = 40}};
        
        public WeatherResult GetWeatherForLocation(int zipCode)
        {
            return wr;
        }
    }
    


    /// <summary>
    /// This is a stub.  Stubs can be used to return or monitor data as needed
    /// </summary>
    public class DisplayServiceStub : IWeatherDisplayService
    {
        public string ShirtType { get; set; } = string.Empty;

        public string GenerateDisplay(WeatherResult weatherResult, string shirtType)
        {
            ShirtType = shirtType;
            return shirtType;
        }
    }
    
    
    /// <summary>
    /// This is a Fake.  We can pass it in to satisfy the constructor requirement, but it doesn't actually do anything
    /// </summary>
    public class FakeLogger : Microsoft.Extensions.Logging.ILogger<ShirtService>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //do nothing
        }
   }
}