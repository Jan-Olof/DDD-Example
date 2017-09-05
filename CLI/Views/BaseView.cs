using ApplicationLayer.Factories;
using CLI.Interfaces;
using InfrastructureLayer.Helpers.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace CLI.Views
{
    internal class BaseView
    {
        private const string Add = "add-";
        private const string AddPerson = "addperson-";
        private const string All = "all-";
        private const string Delete = "delete-";
        private const string One = "one-";
        private const string RemovePerson = "removeperson-";
        private const string Search = "search-";
        private const string Update = "update-";

        private readonly ILogger _logger;
        private readonly IProductView _productView;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseView"/> class.
        /// </summary>
        public BaseView(ILogger<BaseView> logger, IProductView productView)
        {
            _productView = productView ?? throw new ArgumentNullException(nameof(productView));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Flow()
        {
            try
            {
                _logger.LogInformation(EventIdFactory.UiEventId(), "Starting CLI application.");

                bool isEnd = false;

                while (!isEnd)
                {
                    Console.Write("CLI> ");

                    string readLine = Console.ReadLine();
                    isEnd = HandleUserInput(readLine);
                }

                _logger.LogInformation(EventIdFactory.UiEventId(), "Ending CLI application.");
            }
            catch (Exception e)
            {
                _logger.LogErrorWithInnerExceptions(e);
                Console.WriteLine();
                Console.WriteLine($"A command failed with the following error meassage: {e.Message}");
                Console.ReadLine();
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
            if (input.Contains(Add))
            {
                _productView.AddProduct();
            }

            if (input.Contains(Update))
            {
                _productView.UpdateProduct();
            }

            if (input.Contains(Delete))
            {
                _productView.DeleteProduct();
            }

            if (input.Contains(One))
            {
                _productView.GetProduct();
            }

            if (input.Contains(All))
            {
                _productView.GetProducts();
            }

            if (input.Contains(Search))
            {
                _productView.SearchProducts();
            }

            if (input.Contains(AddPerson))
            {
                _productView.AddPerson();
            }

            if (input.Contains(RemovePerson))
            {
                _productView.RemovePerson();
            }
        }
    }
}