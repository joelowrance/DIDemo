using System.Security.Cryptography.X509Certificates;
using DependencyServices;
using DepenencyModels;

namespace IoCContainers
{
    //What Is Dependency Injection
    public class Explanation
    {
        /*
         *  Dependency Inversion
             * Dependency Inversion is a design guideline that states that
             *  a) High-level modules should not depend on low-level modules. Both should depend on abstractions.
             *  b) Abstractions should not depend on details. Details should depend on abstractions.
             *
             * Makes up part of well known 5 letter acronym SOLID (https://stackify.com/solid-design-principles/)
             */

        /*
         * The definition above is very vague, so here's some code.
         *
         * This is dependency injection, but not dependency inversion
         */
        public class MyService
        {
            private OpenWeatherMapService _weatherService;

            public MyService(OpenWeatherMapService weatherService)
            {
                _weatherService = weatherService;
            }
        }

        /* This is dependency injection and dependency inversion.  We are now depending on an abstraction */
        public class MyBetterService
        {
            private IWeatherService _weatherService;

            public MyBetterService(IWeatherService weatherService)
            {
                _weatherService = weatherService;
            }
        }
        
        /*
         * Other jargon https://stackoverflow.com/a/6551303/190592
         * IoC is a generic term meaning rather than having the application call the methods in a framework, the framework calls implementations provided by the application.
         * 
         * DI is a form of IoC, where implementations are passed into an object through constructors/setters/service lookups, which the object will 'depend' on in order to behave correctly.
         * 
         * IoC without using DI, for example would be the Template pattern because the implementation can only be changed through sub-classing.
         * 
         * DI Frameworks are designed to make use of DI and can define interfaces (or Annotations in Java) to make it easy to pass in the implementations.
         * 
         * IoC Containers are DI frameworks that can work outside of the programming language. In some you can configure which implementations to use in metadata files (e.g. XML) which are less invasive. With some you can do IoC that would normally be impossible like inject an implementation at pointcuts.
         */
    }
}