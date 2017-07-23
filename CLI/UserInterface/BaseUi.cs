using System;
using CLI.Configure;
using CLI.Controllers;

namespace CLI.UserInterface
{
    internal class BaseUi
    {
        private readonly DependencyScope _dependencyScope;

        public BaseUi(DependencyScope dependencyScope)
        {
            _dependencyScope = dependencyScope ?? throw new ArgumentNullException(nameof(dependencyScope));
        }

        public void Flow()
        {
            bool isEnd = false;

            while (!isEnd)
            {
                Console.Write("CLI> ");

                string readLine = Console.ReadLine();
                isEnd = HandleUserInput(readLine);
            }
        }

        private bool HandleUserInput(string readLine)
        {
            string input = readLine.ToLower();

            if (input.Equals("exit"))
            {
                return true;
            }

            if (input.Contains("-product") || input.Contains("-products"))
            {
                ProductCommands(input);
            }

            return false;
        }

        private void ProductCommands(string input)
        {
            var productUi = new ProductUi(_dependencyScope);

            if (input.Contains("add-product"))
            {
                productUi.AddProduct(input);
            }

            if (input.Contains("one-product"))
            {
                productUi.GetProduct(input);
            }

            if (input.Contains("all-products"))
            {
                productUi.GetProducts(input);
            }

            productUi.Dispose();
        }
    }
}