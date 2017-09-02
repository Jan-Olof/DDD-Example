using DomainLayer.Models;
using System;

namespace CLI.Interfaces
{
    public interface IProductView : IDisposable
    {
        void AddPerson();

        void AddProduct();

        void DeleteProduct();

        Product GetProduct();

        void GetProducts();

        void RemovePerson();

        void SearchProducts();

        void UpdateProduct();
    }
}