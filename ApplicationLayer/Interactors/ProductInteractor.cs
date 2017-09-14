using ApplicationLayer.Interfaces.Infrastructure;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Enums;
using DomainLayer.Exceptions;
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
    public class ProductInteractor : IProductInteractor
    {
        private readonly ICommands _commands;
        private readonly ILogger _logger;
        private readonly IQueries _queries;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductInteractor"/> class.
        /// </summary>
        public ProductInteractor(IQueries queries, ICommands commands, ILogger<ProductInteractor> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        /// <summary>
        /// Add a person to a product.
        /// </summary>
        public void AddPersonToProduct(int personId, int productId, Role role)
        {
            try
            {
                var person = _queries.GetPerson(personId);
                Entity.CheckNull(person);

                var product = _queries.GetProduct(productId);
                Entity.CheckNull(product);

                var personInProduct = product.Persons.SingleOrDefault(PersonInProduct.Get(personId, role));
                Entity.CheckNotNull(personInProduct);

                product.Persons.Add(PersonFactory.CreatePersonInProduct(personId, person.Name, role));
                _commands.UpdateProduct(product);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is TooManyFoundException)
            {
                _logger.LogWarning($"AddPersonToProduct failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        public Product CreateProduct(string name, string description = "")
        {
            var product = ProductFactory.CreateProduct(name, description);

            return _commands.AddProduct(product);
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void DeleteProduct(int id)
            => _commands.RemoveProduct(id);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _commands?.Dispose();
            _queries?.Dispose();
        }

        /// <summary>
        /// Get product by id.
        /// </summary>
        public Product GetProduct(int id)
            => _queries.GetProduct(id);

        /// <summary>
        /// Get all products.
        /// </summary>
        public List<Product> GetProducts()
            => _queries.GetProducts();

        /// <summary>
        /// Get product by name.
        /// </summary>
        public List<Product> GetProducts(string name)
            => _queries.GetProducts(name);

        /// <summary>
        /// Remove a person from a product.
        /// </summary>
        public void RemovePersonFromProduct(int personId, int productId, Role role)
        {
            try
            {
                var product = _queries.GetProduct(productId);
                Entity.CheckNull(product);

                var personInProduct = product.Persons.SingleOrDefault(PersonInProduct.Get(personId, role));
                Entity.CheckNull(personInProduct);

                product.Persons.Remove(personInProduct);

                _commands.UpdateProduct(product);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning($"RemovePersonFromProduct failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Search product by name.
        /// </summary>
        public List<Product> SearchProducts(string name)
            => _queries.GetProducts(name, true);

        /// <summary>
        /// Update a product.
        /// </summary>
        public Product UpdateProduct(int id, string name, string description = "")
        {
            var product = ProductFactory.CreateProduct(name, description, id);

            _commands.UpdateProduct(product);

            return _queries.GetProduct(product.Id);
        }
    }
}