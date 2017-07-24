// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedParameter.Global

using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace API
{
    /// <summary>
    /// Program is the start class for .NET Core.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method.
        /// </summary>
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}