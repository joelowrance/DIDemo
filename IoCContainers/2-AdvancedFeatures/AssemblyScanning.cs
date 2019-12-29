using System;
using DependencyServices;

namespace IoCContainers
{
    public class AssemblyScanning
    {
        public void Demo()
        {
            var container2 = new Lamar.Container(_ =>
            {
                _.Scan(_scan =>
                {
                    _scan.Assembly(typeof(IDemoService).Assembly);
                    _scan.WithDefaultConventions();
                });
            });
            
            var demo2 = container2.GetInstance<DemoService>();
            var temp2 = demo2.WhatShouldIWear(21144);
            Console.WriteLine(temp2);
        }
    }
}