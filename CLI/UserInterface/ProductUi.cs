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

        public void AddProduct(string input)
        {
            Console.Write("Name? ");
            _controller.Product.Name = Console.ReadLine();

            Console.Write("Description? ");
            _controller.Product.Description = Console.ReadLine();

            _controller.CreateProduct();

            Console.WriteLine($"Product created with id { _controller.Product.Id}.");
        }

        public void Dispose()
        {
            _controller?.Dispose();
        }

        public void GetProduct(string input)
        {
            Console.Write("Id? ");
            string line = Console.ReadLine();

            if (int.TryParse(line, out int id))
            {
                _controller.GetProduct(id);
                ShowProduct(_controller.Product);
            }
        }

        public void GetProducts(string input)
        {
            _controller.GetProducts();
            ShowProducts(_controller.Products);
        }

        private static void ShowProduct(Product product)
        {
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