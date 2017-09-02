using CLI.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace CLI.Configure
{
    internal class StartUp : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartUp"/> class.
        /// </summary>
        public StartUp()
            => _serviceProvider = BasicConfiguration();

        public BaseView CreateBaseView()
            => _serviceProvider.GetService<BaseView>();

        public void Dispose()
            => _serviceProvider?.Dispose();

        private static ServiceProvider BasicConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();
            services = ConfigureProgram.ConfigureServices(services, configuration);
            services = ConfigureProgram.ConfigureDependencyInjection(services);

            var serviceProvider = ConfigureProgram.CreateServiceProvider(services);
            ConfigureProgram.ConfigureLogging(serviceProvider);

            return serviceProvider;
        }
    }
}