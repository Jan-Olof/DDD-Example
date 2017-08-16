using System;
using System.Collections.Generic;
using DomainLayer.Models;
using DomainLayer.Enums;

namespace CLI.Controllers
{
    public interface IProductController : IDisposable
    {
        void AddPerson(int prodId, int persId, string role);

        Product CreateProduct(string name, string description = "");

        void DeleteProduct(int id);

        Product GetProduct(string input);

        IList<Product> GetProducts();

        void RemovePerson(int prodId, int persId, string role);

        IList<Product> SearchProducts(string name);

        Product UpdateProduct(int id, string name, string description = "");
    }
}