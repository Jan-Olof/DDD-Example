using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Factories;
using DomainLayer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CLI.Controllers
{
    public class ProductController : BaseController, IProductController
    {
        private readonly IProductInteractor _productInteractor;

        public ProductController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _productInteractor = serviceProvider.GetService<IProductInteractor>();
        }

        public Product CreateProduct(string name, string description = "")
        {
            var product = ProductFactory.CreateProduct(name, description);

            return _productInteractor.Create(product);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            _productInteractor?.Dispose();
        }

        public Product GetProduct(int id)
        {
            return _productInteractor.Get(id);
        }

        public Product GetProduct(string name)
        {
            return _productInteractor.Get(name).SingleOrDefault();
        }

        public IList<Product> GetProducts()
        {
            return _productInteractor.Get();
        }

        //private void UpdateProduct()
        //{
        //    Console.WriteLine("Id of product to update?");
        //    string idEntered = Console.ReadLine();

        //    bool ok = int.TryParse(idEntered, out int id);

        //    if (!ok)
        //    {
        //        Console.WriteLine("Failed to parse entered id.");
        //        return;
        //    }

        //    var product = _productInteractor.Get(id);

        //    if (product == null)
        //    {
        //        Console.WriteLine("Failed to get product from id.");
        //        return;
        //    }

        //    Console.WriteLine($"Old description: {product.Description}");
        //    Console.WriteLine("Enter new description:");
        //    string description = Console.ReadLine();

        //    product.Description = description;
        //    _productInteractor.Update(product);

        //    Console.WriteLine("New description entered.");
        //}

        //private void ViewProducts()
        //{
        //    var products = _productInteractor.Get();

        //    if (products == null || !products.Any())
        //    {
        //        Console.WriteLine("There are no products.");
        //        return;
        //    }

        //    Console.WriteLine("Showing all products:/n");

        //    foreach (var product in products)
        //    {
        //        Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Description: {product.Description}");
        //        Console.WriteLine();
        //    }
        //}
    }
}