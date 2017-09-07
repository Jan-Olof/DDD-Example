using ApplicationLayer.Exceptions;
using ApplicationLayer.Interfaces.Infrastructure;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Enums;
using DomainLayer.Factories;
using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationLayer.Interactors
{
    /// <summary>
    /// The product interactor class. Handles the stories/tasks concerning products.
    /// </summary>
    public class ProductInteractor : BaseInteractor, IProductInteractor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductInteractor"/> class.
        /// </summary>
        public ProductInteractor(IDomainRepository repository, ILogger<ProductInteractor> logger)
            : base(logger, repository)
        { }

        /// <summary>
        /// Add a person to a product.
        /// </summary>
        public void AddPersonToProduct(int personId, int productId, Role role)
        {
            try
            {
                var person = Repository.GetPerson(personId);
                CheckNull(person);

                var product = Repository.GetProduct(productId);
                CheckNull(product);

                var personInProduct = product.Persons.SingleOrDefault(PersonInProduct.Get(personId, role));
                CheckNotNull(personInProduct);

                product.Persons.Add(PersonFactory.CreatePersonInProduct(personId, person.Name, role));
                Repository.UpdateProduct(product);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is TooManyFoundException)
            {
                Logger.LogWarning($"AddPersonToProduct failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        public Product CreateProduct(string name, string description = "")
        {
            var product = ProductFactory.CreateProduct(name, description);

            return Repository.AddProduct(product);
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void DeleteProduct(int id)
            => Repository.RemoveProduct(id);

        /// <summary>
        /// Get product by id.
        /// </summary>
        public Product GetProduct(int id)
            => Repository.GetProduct(id);

        /// <summary>
        /// Get all products.
        /// </summary>
        public List<Product> GetProducts()
            => Repository.GetProducts().ToList();

        /// <summary>
        /// Get product by name.
        /// </summary>
        public List<Product> GetProducts(string name)
            => Repository.GetProducts(name).ToList();

        /// <summary>
        /// Remove a person from a product.
        /// </summary>
        public void RemovePersonFromProduct(int personId, int productId, Role role)
        {
            try
            {
                var product = Repository.GetProduct(productId);

                CheckNull(product);

                var personInProduct = product.Persons.SingleOrDefault(PersonInProduct.Get(personId, role));

                CheckNull(personInProduct);

                product.Persons.Remove(personInProduct);

                Repository.UpdateProduct(product);
            }
            catch (ArgumentNullException ex)
            {
                Logger.LogWarning($"RemovePersonFromProduct failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Search product by name.
        /// </summary>
        public List<Product> SearchProducts(string name)
            => Repository.GetProducts(name, true).ToList();

        /// <summary>
        /// Update a product.
        /// </summary>
        public Product UpdateProduct(int id, string name, string description = "")
        {
            var product = ProductFactory.CreateProduct(name, description, id);

            Repository.UpdateProduct(product);

            return Repository.GetProduct(product.Id);
        }
    }
}