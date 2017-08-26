// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable MemberCanBePrivate.Global

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace API
{
    /// <summary>
    /// Program is the start class for .NET Core.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Builds the WebHost instance.
        /// This method is used (by convention) by EF Core to access the application's service provider at design time.
        /// </summary>
        public static IWebHost BuildWebHost(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        /// <summary>
        /// The main method.
        /// </summary>
        public static void Main(string[] args)
            => BuildWebHost(args).Run();
    }
}