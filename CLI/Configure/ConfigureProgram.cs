using System;
using System.Collections.Generic;
using ApplicationLayer.Interactors;
using ApplicationLayer.Interfaces.Infrastructure;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Repositories;
using InfrastructureLayer.DataAccess.SqlServer;
using InfrastructureLayer.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace CLI.Configure
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
            services.AddTransient<IProductFunctions, Product>();
            services.AddTransient<IProductProps, Product>();
            services.AddTransient<IProduct, Product>();
            services.AddTransient<IList<Product>, List<Product>>();
            services.AddTransient<IUpdateMapper<Product>, Product>();

            services.AddTransient<IPersonFunctions, Person>();
            services.AddTransient<IPersonProps, Person>();
            services.AddTransient<IPerson, Person>();
            services.AddTransient<IList<Person>, List<Person>>();
            services.AddTransient<IUpdateMapper<Person>, Person>();

            services.AddTransient<IProductPersonFunctions, ProductPerson>();
            services.AddTransient<IProductPersonProps, ProductPerson>();
            services.AddTransient<IProductPerson, ProductPerson>();
            services.AddTransient<IList<ProductPerson>, List<ProductPerson>>();

            services.AddTransient<IProductInteractor, ProductInteractor>();

            services.AddTransient<ILogger<ProductInteractor>, Logger<ProductInteractor>>();
            services.AddTransient<IJsonSerialization, JsonSerialization>();
            services.AddTransient<IFileHandler<IList<Product>>, FileHandler<IList<Product>>>();

            services.AddTransient<DbContext, ExampleContext>();
            services.AddTransient<IDomainRepository, EfDomainRepository>();
            //services.AddTransient<IDomainRepository, InMemoryRepository>();

            return services;
        }

        /// <summary>
        /// Configure the logging for the application.
        /// </summary>
        public static void ConfigureLogging(IServiceProvider serviceProvider)
        {
            serviceProvider
                 .GetService<ILoggerFactory>()
                //.AddConsole()
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

            //services.Configure<Datafile>(options => configuration.GetSection("datafile").Bind(options));

            string connection = configuration["database:connectionstring"];
            services.AddDbContext<ExampleContext>(options => options.UseSqlServer(connection));

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