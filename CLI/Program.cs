// ReSharper disable  ClassNeverInstantiated.Global
// ReSharper disable  UnusedMember.Local
// ReSharper disable  UnusedParameter.Local

using System;
using CLI.Configure;
using CLI.Controllers;
using Microsoft.Extensions.Logging;

using static CLI.ConsoleCommands;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            Console.WriteLine($"datafile = {configuration["datafile"]}");

            var dependencyScope = new DependencyScope(configuration);

            var logger = dependencyScope.CreateLogger();

            logger.LogInformation("Starting application");

            if (YesNoCommand("Start instruction service (y/n)?"))
            {
                var instructionController = (InstructionController)dependencyScope.GetService(typeof(InstructionController));

                instructionController.InstructionFlow();
            }

            logger.LogInformation("All done!");

            Console.ReadKey();
        }
    }
}