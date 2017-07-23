using System;

namespace CLI.Controllers
{
    public interface IProductController : IDisposable
    {
        void CreateProduct();

        void GetProduct(int id);

        void GetProducts();
    }
}