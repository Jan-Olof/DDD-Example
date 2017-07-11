using System;
using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ApplicationLayer.Interfaces.Infrastructure;
using InfrastructureLayer.Files;
using System.Collections.Generic;
using ApplicationLayer.Interactors;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Interfaces;

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
        public static IServiceCollection ConfigureDependencyInjection(IServiceCollection services)
        {
            //services.AddTransient<DbContext, ExampleContext>();
            services.AddTransient<ILogger<ProductInteractor>, Logger<ProductInteractor>>();
            services.AddTransient<IJsonSerialization, JsonSerialization>();
            services.AddTransient<IProductFunctions, Product>();
            services.AddTransient<IProduct, Product>();
            services.AddTransient<IList<Product>, List<Product>>();
            services.AddTransient<IUpdateMapper<Product>, Product>();
            services.AddTransient<IFileHandler<IList<IProduct>>, FileHandler<IList<IProduct>>>();
            services.AddTransient<IRepository<Product>, InMemoryRepository>();
            //services.AddTransient<IRepository<IInstruction>, EfRepository<IInstruction, Instruction>>();
            services.AddTransient<IProductInteractor, ProductInteractor>();

            return services;
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
        public static IServiceCollection ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddLogging();
            services.AddOptions();

            services.Configure<Datafile>(options => configuration.GetSection("datafile").Bind(options));

            //var connection = Configuration["database:connectionstring"];
            //services.AddDbContext<ExampleContext>(options => options.UseSqlServer(connection));

            return services;
        }

        /// <summary>
        /// Create the service provider.
        /// </summary>
        public static IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }
    }
}