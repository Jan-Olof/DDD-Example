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

        private void AddProduct(string input)
        {
            var controller = (ProductController)_dependencyScope.GetService(typeof(ProductController));

            Console.WriteLine("Name?");
            controller.Product.Name = Console.ReadLine();

            Console.WriteLine("Description?");
            controller.Product.Description = Console.ReadLine();

            controller.CreateProduct();

            Console.WriteLine($"Product created with id { controller.Product.Id}.");

            controller.Dispose();
        }

        private bool HandleUserInput(string readLine)
        {
            string input = readLine.ToLower();

            if (input.Equals("exit"))
            {
                return true;
            }

            if (input.Contains("add-product"))
            {
                AddProduct(input);
            }

            return false;
        }
    }
}