using System;
using System.Collections.Generic;
using DomainLayer.Models;

namespace CLI.Controllers
{
    public interface IProductController : IDisposable
    {
        Product CreateProduct(string name, string description = "");

        Product GetProduct(int id);

        Product GetProduct(string name);

        IList<Product> GetProducts();
    }
}