// ReSharper disable  ClassNeverInstantiated.Global
// ReSharper disable  UnusedMember.Local
// ReSharper disable  UnusedParameter.Local

using ApplicationLayer.Factories;
using CLI.Configure;
using CLI.UserInterface;
using Microsoft.Extensions.Logging;

namespace CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dependencyScope = new DependencyScope();

            var logger = dependencyScope.CreateLogger();

            logger.LogInformation(EventIdFactory.CreateUiEventId(), "Starting CLI application.");

            var ui = new BaseUi(dependencyScope);

            ui.Flow();

            logger.LogInformation(EventIdFactory.CreateUiEventId(), "Ending CLI application.");
        }
    }
}