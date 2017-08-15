using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Enums;
using DomainLayer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CLI.Controllers
{
    public class ProductController : BaseController, IProductController
    {
        private readonly IProductInteractor _productInteractor;

        public ProductController(IServiceProvider serviceProvider) : base(serviceProvider)
            => _productInteractor = serviceProvider.GetService<IProductInteractor>();

        public void AddPerson(int prodId, int persId, Role role)
            => _productInteractor.AddPersonToProduct(persId, prodId, role);

        public Product CreateProduct(string name, string description = "")
            => _productInteractor.CreateProduct(name, description);

        public void DeleteProduct(int id)
            => _productInteractor.DeleteProduct(id);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
            => _productInteractor?.Dispose();

        public Product GetProduct(string input)
            => int.TryParse(input, out int id)
                ? _productInteractor.GetProduct(id)
                : _productInteractor.GetProducts(input).SingleOrDefault();

        public IList<Product> GetProducts()
            => _productInteractor.GetProducts();

        public void RemovePerson(int prodId, int persId, Role role)
            => _productInteractor.RemovePersonFromProduct(persId, prodId, role);

        public IList<Product> SearchProducts(string name)
            => _productInteractor.SearchProducts(name);

        public Product UpdateProduct(int id, string name, string description = "")
            => _productInteractor.UpdateProduct(id, name, description);
    }
}