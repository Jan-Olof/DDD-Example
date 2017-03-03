using System;

namespace CLI
{
    public static class ConsoleCommands
    {
        public static bool YesNoCommand(string displayText)
        {
            Console.WriteLine();
            Console.WriteLine(displayText);
            var readKey = Console.ReadKey();
            Console.WriteLine();
            return readKey.Key == ConsoleKey.Y;
        }
    }
}