using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Interactors;
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
            return _productInteractor.Create(name, description);
        }

        public void DeleteProduct(int id)
        {
            _productInteractor.Delete(id);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            _productInteractor?.Dispose();
        }

        public Product GetProduct(string input)
        {
            return int.TryParse(input, out int id)
                ? _productInteractor.Get(id)
                : _productInteractor.Get(input).SingleOrDefault();
        }

        public IList<Product> GetProducts()
        {
            return _productInteractor.Get();
        }

        public IList<Product> SearchProducts(string name)
        {
            return _productInteractor.Search(name);
        }

        public Product UpdateProduct(int id, string name, string description = "")
        {
            return _productInteractor.Update(id, name, description);
        }
    }
}