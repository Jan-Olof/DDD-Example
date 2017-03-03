using System;
using CLI.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

using static InfrastructureLayer.Configure.ConfigureProgram;

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
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection = ConfigureServices(serviceCollection);
            serviceCollection = ConfigureDependencyInjection(serviceCollection);

            var serviceProvider = CreateServiceProvider(serviceCollection);
            ConfigureLogging(serviceProvider);

            return serviceProvider;
        }
    }
}