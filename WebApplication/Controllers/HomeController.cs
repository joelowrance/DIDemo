using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DependencyServices;
using DependencyServices.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IShirtService _shirtService;

        public HomeController(ILogger<HomeController> logger, IShirtService shirtService)
        {
            _logger = logger;
            _shirtService = shirtService;
        }

        public IActionResult Index()
        {
            var data = TempData["weather"] as string;
            return View("Index", data);
        }
        
        [HttpPost]
        public IActionResult Post(int zipCode)
        {
            TempData["weather"] =_shirtService.WhatShouldIWear(zipCode);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}