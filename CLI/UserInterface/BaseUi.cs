using System;
using CLI.Configure;

namespace CLI.UserInterface
{
    internal class BaseUi
    {
        private const string Add = "add-";
        private const string AddPerson = "addperson-";
        private const string All = "all-";
        private const string Delete = "delete-";
        private const string One = "one-";
        private const string RemovePerson = "removeperson-";
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

            try
            {
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
                    productUi.DeleteProduct();
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

                if (input.Contains(AddPerson))
                {
                    productUi.AddPerson();
                }

                if (input.Contains(RemovePerson))
                {
                    productUi.RemovePerson();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine($"A product command failed with the following error meassage: {e.Message}");
            }
            finally
            {
                productUi.Dispose();
            }
        }
    }
}