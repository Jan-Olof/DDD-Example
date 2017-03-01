using System;
using CLI.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using static InfrastructureLayer.Configure.ConfigureProgram;

namespace CLI
{
    internal class Program
    {
        private static bool ConsoleCommand(string displayText)
        {
            Console.WriteLine();
            Console.WriteLine(displayText);
            var readKey = Console.ReadKey();
            Console.WriteLine();
            return readKey.Key == ConsoleKey.Y;
        }

        private static void InstructionHandling(IServiceProvider serviceProvider)
        {
            var instructionController = new InstructionController(serviceProvider);

            if (ConsoleCommand("Add instruction? (y/n)"))
            {
                instructionController.CreateInstruction();
            }

            if (ConsoleCommand("View instructions? (y/n)"))
            {
                instructionController.ViewInstructions();
            }
        }

        // ReSharper disable once UnusedMember.Local
        private static void Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection = ConfigureServices(serviceCollection);
            serviceCollection = ConfigureDependencyInjection(serviceCollection);

            var serviceProvider = ConfigureServiceProvider(serviceCollection);

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogInformation("Starting application");

            if (ConsoleCommand("Start instruction service (y/n)?"))
            {
                InstructionHandling(serviceProvider);
            }

            logger.LogInformation("All done!");

            Console.ReadKey();
        }
    }
}