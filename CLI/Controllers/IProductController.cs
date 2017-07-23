using System;
using System.Collections.Generic;
using DomainLayer.Models;

namespace CLI.Controllers
{
    public interface IProductController : IDisposable
    {
        Product CreateProduct(string name, string description = "");

        Product DeleteProduct(int id);

        Product GetProduct(int id);

        Product GetProduct(string name);

        IList<Product> GetProducts();

        IList<Product> SearchProducts(string name);

        Product UpdateProduct(int id, string name, string description = "");
    }
}