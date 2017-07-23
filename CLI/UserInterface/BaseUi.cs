using System;
using CLI.Configure;

namespace CLI.UserInterface
{
    internal class BaseUi
    {
        private const string Add = "add-";
        private const string All = "all-";
        private const string Delete = "delete-";
        private const string One = "one-";
        private const string Search = "search-";
        private const string Update = "update-";

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

            if (input.Contains("-product"))
            {
                ProductCommands(input);
            }

            return false;
        }

        private void ProductCommands(string input)
        {
            var productUi = new ProductUi(_dependencyScope);

            if (input.Contains(Add))
            {
                productUi.AddProduct();
            }

            if (input.Contains(Update))
            {
                productUi.UpdateProduct();
            }

            if (input.Contains(Delete))
            {
                throw new NotImplementedException();
            }

            if (input.Contains(One))
            {
                productUi.GetProduct();
            }

            if (input.Contains(All))
            {
                productUi.GetProducts();
            }

            if (input.Contains(Search))
            {
                productUi.SearchProducts();
            }

            productUi.Dispose();
        }
    }
}