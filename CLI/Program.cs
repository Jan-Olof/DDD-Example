// ReSharper disable  ClassNeverInstantiated.Global
// ReSharper disable  UnusedMember.Local
// ReSharper disable  UnusedParameter.Local

using CLI.Configure;

namespace CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var startUp = new StartUp())
            {
                startUp.CreateBaseView().Flow();
            }
        }
    }
}