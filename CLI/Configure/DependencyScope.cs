﻿using System;
using CLI.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

using static InfrastructureLayer.Configure.ConfigureProgram;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CLI.Configure
{
    internal class DependencyScope
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyScope()
        {
            _serviceProvider = BasicConfiguration();
        }

        public ILogger<Program> CreateLogger()
        {
            return _serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger<Program>();
        }

        public BaseController GetService(Type serviceType)
        {
            if (serviceType == typeof(InstructionController))
            {
                return new InstructionController(_serviceProvider);
            }

            throw new WrongTypeException($"GetService method does not support the {serviceType} type.");
        }

        private static IServiceProvider BasicConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();
            services = ConfigureServices(services, configuration);
            services = ConfigureDependencyInjection(services);

            var serviceProvider = CreateServiceProvider(services);
            ConfigureLogging(serviceProvider);

            return serviceProvider;
        }
    }
}