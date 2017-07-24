﻿using System;
using System.Collections.Generic;
using CLI.Configure;
using CLI.Controllers;
using DomainLayer.Interfaces;
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
            Console.WriteLine();
        }

        public void DeleteProduct()
        {
            var product = GetProduct();

            if (product == null)
            {
                return;
            }

            Console.Write($"Do you want to delete the product with the name {product.Name}? (y/n) ");
            var key = Console.ReadKey();
            Console.WriteLine();

            if (key.Key == ConsoleKey.Y)
            {
                _controller.DeleteProduct(product.Id);
                Console.WriteLine("The product has been deleted.");
            }

            Console.WriteLine();
        }

        public void Dispose()
        {
            _controller?.Dispose();
        }

        public Product GetProduct()
        {
            Console.Write("Id/Name? ");
            string input = Console.ReadLine();

            var product = _controller.GetProduct(input);

            ShowProduct(product);

            return product;
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

        public void UpdateProduct()
        {
            var product = GetProduct();

            Console.WriteLine();

            string name = GetNameForUpdate(product);

            string description = GetDescriptionForUpdate(product);

            _controller.UpdateProduct(product.Id, name, description);
            Console.WriteLine("The product has been updated.");
            Console.WriteLine();
        }

        private static string GetDescriptionForUpdate(IProductProps product)
        {
            Console.Write("New description? ");
            string description = Console.ReadLine();

            return string.IsNullOrWhiteSpace(description)
                ? HandleEmptyDescription(product)
                : description;
        }

        private static string GetNameForUpdate(Product product)
        {
            Console.Write("New name? ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                name = product.Name;
            }

            return name;
        }

        private static string HandleEmptyDescription(IProductProps product)
        {
            string description = product.Description;

            Console.Write("Should description be blank? (y/n) ");
            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Y)
            {
                description = string.Empty;
            }

            return description;
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
            Console.WriteLine();
        }

        private static void ShowProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                ShowProduct(product);
            }
        }
    }
}