using System;
using System.Collections.Generic;
using CLI.Configure;
using CLI.Controllers;
using DomainLayer.Models;

namespace CLI.UserInterface
{
    internal class ProductUi : IDisposable
    {
        private readonly ProductController _controller;

        public ProductUi(DependencyScope dependencyScope)
        {
            _controller = (ProductController)dependencyScope.GetService(typeof(ProductController));
        }

        public void AddProduct()
        {
            Console.Write("Name? ");
            string name = Console.ReadLine();

            Console.Write("Description? ");
            string description = Console.ReadLine();

            var product = _controller.CreateProduct(name, description);

            Console.WriteLine($"Product created with id { product.Id}.");
        }

        public void Dispose()
        {
            _controller?.Dispose();
        }

        public void GetProduct()
        {
            Console.Write("Id/Name? ");
            string input = Console.ReadLine();

            var product = int.TryParse(input, out int id)
                ? _controller.GetProduct(id)
                : _controller.GetProduct(input);

            ShowProduct(product);
        }

        public void GetProducts()
        {
            var products = _controller.GetProducts();
            ShowProducts(products);
        }

        public void SearchProducts()
        {
            Console.Write("Name to search? ");
            string input = Console.ReadLine();

            var products = _controller.SearchProducts(input);
            ShowProducts(products);
        }

        private static void ShowProduct(Product product)
        {
            if (product == null)
            {
                Console.WriteLine("Could not find the product.");
                return;
            }

            Console.Write("Id: ");
            Console.WriteLine(product.Id);
            Console.Write("Name: ");
            Console.WriteLine(product.Name);
            Console.Write("Description: ");
            Console.WriteLine(product.Description);
        }

        private static void ShowProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                ShowProduct(product);
                Console.WriteLine();
            }
        }
    }
}