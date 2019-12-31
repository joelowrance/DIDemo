using System;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class LifetimeController : Controller
    {
        private SingletonService _singleton;
        private TransientService _transientService;
        private ScopedService _scoped;
        private readonly SomeService _someService;

        public LifetimeController(SingletonService singleton, TransientService transientService, ScopedService scoped, SomeService someService)
        {
            _singleton = singleton;
            _transientService = transientService;
            _scoped = scoped;
            _someService = someService;
        }

        public IActionResult Index()
        {
            Console.WriteLine("Singleton: " + _singleton.Identity); //will be the same every time
            Console.WriteLine("Scoped: " + _scoped.Identity); // will be the same per request TODO:  add a new service that required trhis
            Console.WriteLine("Scoped.Transient: " + _scoped._transientService.Identity); //will be different each time
            Console.WriteLine("Transient: " + _transientService.Identity); 
            Console.WriteLine("SomeService.Scoped: " + _someService._scoped.Identity);
            
            return View();
        }

    }

    public class SomeService
    {
        public ScopedService _scoped;

        public SomeService(ScopedService scoped)
        {
            _scoped = scoped;
        }
    }

    public class SingletonService
    {
        public Guid Identity { get; }

        public SingletonService()
        {
            Identity = Guid.NewGuid();
        }
    }

    public class TransientService
    {
        public Guid Identity { get; }

        public TransientService()
        {
            Identity = Guid.NewGuid();
        }
    }
    
    
    public class ScopedService
    {
        public TransientService _transientService;
        public Guid Identity { get; }

        public ScopedService(TransientService transientService)
        {
            _transientService = transientService;
            Identity = Guid.NewGuid();
        }
    }
}