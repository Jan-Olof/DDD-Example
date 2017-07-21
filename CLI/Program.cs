// ReSharper disable  ClassNeverInstantiated.Global
// ReSharper disable  UnusedMember.Local
// ReSharper disable  UnusedParameter.Local

using System;
using ApplicationLayer.Factories;
using CLI.Configure;
using CLI.Controllers;
using Microsoft.Extensions.Logging;

using static CLI.ConsoleCommands;

namespace CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dependencyScope = new DependencyScope();

            var logger = dependencyScope.CreateLogger();

            logger.LogInformation(EventIdFactory.CreateUiEventId(), "Starting application");

            if (YesNoCommand("Start product controller (y/n)?"))
            {
                var productController = (ProductController)dependencyScope.GetService(typeof(ProductController));

                productController.InstructionFlow();
            }

            logger.LogInformation(EventIdFactory.CreateUiEventId(), "All done!");

            Console.ReadKey();
        }
    }
}