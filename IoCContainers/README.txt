WHAT - See the example class in 1-WhatIsDependencyInjection
------------------------------------------------------------

WHY
------------------------------------------------------------
1. Testable code.  Allows you to break apart complex problems into small isolated units.
  
2. Business/Practical reasons  For example if you had a credit card service that cost you money for each call, you'd want to save money
by not calling this service unless you had to.  Alternately, you might have a service that generates a PDF
that takes a long time to run.  By using an alternate implementation you can shorten the feedback loop
while working on adjacent code.  Or you might be waiting on SSI to finally finish thier API and implementing your own 
version of the agreed upon interface will allow you to move forward.

3.  Lower change overhead.  If the constructor to a class adds a new argument, you don't have to go back and update
every usage eg SimpleWeatherService(Logger) changes SimpleWeatherService(Logger, Location)

4.  Loose coupling.  You can now depend on an abstraction and not an implementation.

5.  Moves control up the call stack.  Dependencies will be resolved at the root of the application which can be helpful 
for troubleshooting.


WHEN 
------------------------------------------------------------
Whenever you start feeling the pain of having to manage dependencies.  More of a gut feel than a hard and fast rule.

HOW
------------------------------------------------------------
Generally you would use as Dependency Injection framework such as Ninject, Lamar, Castle or the built-in version in .net core web applications.  Doing this manually is an option,
but things get complicated pretty quickly.   See the examples in 2-IoCFrameworks