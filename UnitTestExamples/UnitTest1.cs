using System;
using DependencyServices.Abstractions;
using DependencyServices.Services;
using DependencyServices.WeatherModels;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTestExamples
{
    public class UnitTestShirtService : IWeatherService
    {
        private readonly IWeatherService _weatherServiceObject;
        private readonly FakeLogger _logger;

        public UnitTestShirtService(IWeatherService weatherServiceObject, FakeLogger logger)
        {
            _weatherServiceObject = weatherServiceObject;
            _logger = logger;
        }

        public WeatherResult GetWeatherForLocation(int zipCode)
        {
            throw new NotImplementedException();
        }
    }
    
    public class ShirtServiceTests
    {
        [Fact]
        public void WhatShouldIWear_Success_PassesTheZipCodeToTheWeatherService()
        {
            //Arrange
            var logger = new FakeLogger();
            var weatherService = new Mock<IWeatherService>();
            weatherService.Setup(x => x.GetWeatherForLocation(21144)).Returns((WeatherResult) null);
            var shirtService= new UnitTestShirtService(weatherService.Object, logger);
            
            //Act
            shirtService.GetWeatherForLocation(90210);

            //Assert
            weatherService.Verify();

        }
        

        [Fact]
        public void WhatShouldIWear_Success_PassesTheWeatherAndShirtTypeToTheFormatter()
        {
            //Arrange 
            
            //Act
            
            //Assert
        }

        [Fact]
        public void WhatShouldIWear_Success_LogsThatACallWasMade()
        {
            //Arrange 
            
            //Act
            
            //Assert
        }

        [Fact]
        public void WhatShouldIWear_Success_ReturnsAMessageWithAShirtType()
        {
            //Arrange 
            
            //Act
            
            //Assert
        }
        
        [Fact]
        public void WhatShouldIWear_OnException_Logs()
        {
            //Arrange 
            
            //Act
            
            //Assert
        }
        
        [Fact]
        public void WhatShouldIWear_OnException_ReturnsADefaultValue()
        {
            //Arrange 
            
            //Act
            
            //Assert
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
    public class FakeLogger : ILogger<ShirtService>
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