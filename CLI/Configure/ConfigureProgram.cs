using System;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Services;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CLI.Configure
{
    public static class ConfigureProgram
    {
        public static IServiceCollection ConfigureDependencyInjection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging();
            serviceCollection.AddTransient<IInstructionModel, Instruction>();
            serviceCollection.AddTransient<IInstruction, Instruction>();
            serviceCollection.AddTransient<IRepository<IInstruction>, InMemoryRepository<IInstruction>>();
            serviceCollection.AddTransient<IInstructionService, InstructionService>();

            return serviceCollection;
        }

        public static IServiceProvider ConfigureServiceProvider(IServiceCollection serviceCollection)
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole()
                .AddDebug(LogLevel.Trace);

            return serviceProvider;
        }
    }
}