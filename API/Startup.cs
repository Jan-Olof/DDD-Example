// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global

using API.Middleware;
using ApplicationLayer.Interactors;
using ApplicationLayer.Interfaces.Infrastructure;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Repositories;
using InfrastructureLayer.DataAccess.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace API
{
    /// <summary>
    /// The startup class for ASP.NET Core.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            env.ConfigureNLog("nlog.config");
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();
            //add NLog.Web
            app.AddNLogWeb();

            app.UseErrorHandling(loggerFactory.CreateLogger(typeof(ErrorHandling)));

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD-Example API V1");
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            //.AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});

            services.AddLogging();

            string connection = Configuration["database:connectionstring"];
            services.AddDbContext<ExampleContext>(options => options.UseSqlServer(connection));

            //call this in case you need aspnet-user-authtype/aspnet-user-identity
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Dependency injection
            services.AddTransient<DbContext, ExampleContext>();
            services.AddTransient<ICommands, EfDomainRepository>();
            services.AddTransient<IQueries, EfDomainRepository>();
            services.AddTransient<IProduct, Product>();
            services.AddTransient<IProductInteractor, ProductInteractor>();

            ConfigureSwagger(services);
        }

        /// <summary>
        /// Register the Swagger generator, defining one or more Swagger documents.
        /// </summary>
        private static void ConfigureSwagger(IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "DDD-Example API",
                    Version = "v1",
                    Description = "This is the API for the DDD-Example",
                    TermsOfService = "None"
                });

                //Set the comments path for the swagger json and ui.
                string basePath = PlatformServices.Default.Application.ApplicationBasePath;
                string xmlPath = Path.Combine(basePath, "API.xml");
                c.IncludeXmlComments(xmlPath);
            });
    }
}