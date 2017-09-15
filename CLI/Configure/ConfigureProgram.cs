using ApplicationLayer.Infrastructure;
using ApplicationLayer.Products;
using CLI.Controllers;
using CLI.Interfaces;
using CLI.Views;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Repositories;
using InfrastructureLayer.DataAccess.SqlServer;
using InfrastructureLayer.Helpers.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;

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
            services.AddTransient<ILogger<ProductInteractor>, Logger<ProductInteractor>>();
            services.AddTransient<ILogger<BaseView>, Logger<BaseView>>();

            services.AddTransient<IProductDto, Product>();
            services.AddTransient<IProduct, Product>();
            services.AddTransient<IList<Product>, List<Product>>();

            services.AddTransient<IPersonDto, Person>();
            services.AddTransient<IPerson, Person>();
            services.AddTransient<IList<Person>, List<Person>>();

            services.AddTransient<IProductInteractor, ProductInteractor>();

            services.AddTransient<IJsonSerialization, JsonSerialization>();
            services.AddTransient<IFileHandler<IList<Product>>, FileHandler<IList<Product>>>();

            services.AddTransient<DbContext, ExampleContext>();
            services.AddTransient<ICommands, EfDomainRepository>();
            services.AddTransient<IQueries, EfDomainRepository>();
            //services.AddTransient<IDomainRepository, InMemoryRepository>();

            services.AddTransient<IProductView, ProductView>();
            services.AddTransient<IProductController, ProductController>();

            services.AddTransient(typeof(BaseView));

            return services;
        }

        /// <summary>
        /// Configure the logging for the application.
        /// </summary>
        public static void ConfigureLogging(IServiceProvider serviceProvider)
            => serviceProvider
                .GetService<ILoggerFactory>()
                .AddDebug(LogLevel.Trace)
                .AddNLog()
                .ConfigureNLog("nlog.config");

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
        public static ServiceProvider CreateServiceProvider(IServiceCollection services)
            => services.BuildServiceProvider();
    }
}