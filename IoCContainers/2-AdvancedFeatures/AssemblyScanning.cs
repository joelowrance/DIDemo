﻿using System;
using DependencyServices.Abstractions;
using DependencyServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IoCContainers
{
    public class AssemblyScanning
    {
        public void Demo()
        {
            var container2 = new Lamar.Container(containerConfig =>
            {
                containerConfig.AddLogging();
                containerConfig.Scan(scan =>
                {
                    scan.Assembly(typeof(IShirtService).Assembly);
                    scan.WithDefaultConventions(); // names must match, ie, ISomethingService must have a matching SomethingService 
                });
            });
            
            var demo2 = container2.GetInstance<ShirtService>();
            var temp2 = demo2.WhatShouldIWear(21144);
            Console.WriteLine(temp2);
        }
    }
}