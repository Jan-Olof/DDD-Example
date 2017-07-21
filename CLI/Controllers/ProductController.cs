using System;
using System.Linq;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CLI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductInteractor _productService;

        public ProductController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _productService = serviceProvider.GetService<IProductInteractor>();
        }

        public void InstructionFlow()
        {
            if (ConsoleCommands.YesNoCommand("Add product? (y/n)"))
            {
                CreateProduct();
            }

            if (ConsoleCommands.YesNoCommand("View products? (y/n)"))
            {
                ViewProducts();
            }

            if (ConsoleCommands.YesNoCommand("Update product description? (y/n)"))
            {
                UpdateProduct();
            }

            if (ConsoleCommands.YesNoCommand("View products? (y/n)"))
            {
                ViewProducts();
            }
        }

        private void CreateProduct()
        {
            var product = new Product();

            Console.WriteLine("Name?");
            product.Name = Console.ReadLine();

            Console.WriteLine("Description?");
            product.Description = Console.ReadLine();

            var createdProduct = _productService.Create(product);

            Console.WriteLine($"Product created with id {createdProduct.Id}.");
        }

        private void UpdateProduct()
        {
            Console.WriteLine("Id of product to update?");
            string idEntered = Console.ReadLine();

            bool ok = int.TryParse(idEntered, out int id);

            if (!ok)
            {
                Console.WriteLine("Failed to parse entered id.");
                return;
            }

            var product = _productService.Get(id);

            if (product == null)
            {
                Console.WriteLine("Failed to get product from id.");
                return;
            }

            Console.WriteLine($"Old description: {product.Description}");
            Console.WriteLine("Enter new description:");
            string description = Console.ReadLine();

            product.Description = description;
            _productService.Update(product);

            Console.WriteLine("New description entered.");
        }

        private void ViewProducts()
        {
            var products = _productService.Get();

            if (products == null || !products.Any())
            {
                Console.WriteLine("There are no products.");
                return;
            }

            Console.WriteLine("Showing all products:/n");

            foreach (var product in products)
            {
                Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Description: {product.Description}");
                Console.WriteLine();
            }
        }
    }
}