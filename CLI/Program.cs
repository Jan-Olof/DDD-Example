// ReSharper disable  ClassNeverInstantiated.Global
// ReSharper disable  UnusedParameter.Local

using CLI.Configure;
using static Helpers.Functional.F;

namespace CLI
{
    internal class Program
    {
        private static void Main(string[] args) => Using(new StartUp(), startUp => startUp.CreateBaseView().Flow());
    }
}