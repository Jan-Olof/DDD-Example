//using System;
//using ApplicationLayer.Interfaces;
//using ApplicationLayer.Interfaces.Models;
//using ApplicationLayer.Interfaces.Services;
//using ApplicationLayer.Services;
//using DomainLayer.Models;
//using InfrastructureLayer.DataAccess.Repositories;
//using InfrastructureLayer.DataAccess.SqlServer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;

//namespace CLI.Configure
//{
//    public static class ConfigureProgram
//    {
//        public static IServiceCollection ConfigureDependencyInjection(IServiceCollection serviceCollection)
//        {
//            // IoC container
//            serviceCollection.AddTransient<DbContext, ExampleContext>();
//            serviceCollection.AddTransient<ILogger<InstructionService>, Logger<InstructionService>>();
//            serviceCollection.AddTransient<IInstructionModel, Instruction>();
//            serviceCollection.AddTransient<IInstruction, Instruction>();
//            //serviceCollection.AddTransient<IRepository<IInstruction>, InMemoryRepository<IInstruction>>();
//            serviceCollection.AddTransient<IRepository<IInstruction>, EfRepository<IInstruction, Instruction>>();
//            serviceCollection.AddTransient<IInstructionService, InstructionService>();

//            return serviceCollection;
//        }

//        public static IServiceProvider ConfigureServiceProvider(IServiceCollection serviceCollection)
//        {
//            var serviceProvider = serviceCollection.BuildServiceProvider();

//            //configure console logging
//            serviceProvider
//                .GetService<ILoggerFactory>()
//                .AddConsole()
//                .AddDebug(LogLevel.Trace);

//            return serviceProvider;
//        }

//        public static IServiceCollection ConfigureServices(IServiceCollection serviceCollection)
//        {
//            serviceCollection.AddLogging();

//            var connection = @"Server=localhost\sql2016;Database=EfExampleDatabase;Trusted_Connection=True;";
//            serviceCollection.AddDbContext<ExampleContext>(options => options.UseSqlServer(connection));

//            return serviceCollection;
//        }
//    }
//}