using System;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Interfaces.Models;
using ApplicationLayer.Interfaces.Services;
using ApplicationLayer.Services;
using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Repositories;
using InfrastructureLayer.DataAccess.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace InfrastructureLayer.Configure
{
    /// <summary>
    /// Configure a .NET Core application.
    /// </summary>
    public static class ConfigureProgram
    {
        /// <summary>
        /// The IoC container.
        /// </summary>
        public static IServiceCollection ConfigureDependencyInjection(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<DbContext, ExampleContext>();
            serviceCollection.AddTransient<ILogger<InstructionService>, Logger<InstructionService>>();
            serviceCollection.AddTransient<IInstructionModel, Instruction>();
            serviceCollection.AddTransient<IInstruction, Instruction>();
            serviceCollection.AddTransient<IUpdateMapper<IInstruction>, Instruction>();
            //serviceCollection.AddTransient<IRepository<IInstruction>, InMemoryRepository<IInstruction>>();
            serviceCollection.AddTransient<IRepository<IInstruction>, EfRepository<IInstruction, Instruction>>();
            serviceCollection.AddTransient<IInstructionService, InstructionService>();

            return serviceCollection;
        }

        /// <summary>
        /// Configure the logging for the application.
        /// </summary>
        public static void ConfigureLogging(IServiceProvider serviceProvider)
        {
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole()
                .AddDebug(LogLevel.Trace)
                .AddNLog()
                .ConfigureNLog("nlog.config");
        }

        /// <summary>
        /// Add and configure extension services with dependency injection.
        /// </summary>
        public static IServiceCollection ConfigureServices(IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.AddLogging();
            services.AddOptions();

            services.Configure<DataFile>(options => Configuration.GetSection("datafile").Bind(options));

            var connection = Configuration["database:connectionstring"];
            services.AddDbContext<ExampleContext>(options => options.UseSqlServer(connection));

            return services;
        }

        /// <summary>
        /// Create the service provider.
        /// </summary>
        public static IServiceProvider CreateServiceProvider(IServiceCollection serviceCollection)
        {
            return serviceCollection.BuildServiceProvider();
        }
    }
}