using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Interfaces.Infrastructure;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Enums;
using DomainLayer.Factories;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using static ApplicationLayer.Factories.EventIdFactory;

namespace ApplicationLayer.Interactors
{
    /// <summary>
    /// The product interactor class. Handles the stories/tasks concerning products.
    /// </summary>
    public class ProductInteractor : BaseInteractor, IProductInteractor
    {
        private readonly IProduct _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductInteractor"/> class.
        /// </summary>
        public ProductInteractor(IDomainRepository repository, IProduct model, ILogger<ProductInteractor> logger)
            : base(logger, repository)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

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
            catch (Exception e)
            {
                Logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        public Product CreateProduct(string name, string description = "")
        {
            try
            {
                var product = ProductFactory.CreateProduct(name, description);

                var insertedProduct = Repository.AddProduct(product);

                return insertedProduct;
            }
            catch (Exception e)
            {
                Logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void DeleteProduct(int id)
        {
            try
            {
                Repository.RemoveProduct(id);
            }
            catch (Exception e)
            {
                Logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get product by id.
        /// </summary>
        public Product GetProduct(int id)
        {
            try
            {
                return Repository.GetProduct(id);
            }
            catch (Exception e)
            {
                Logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        public List<Product> GetProducts()
        {
            try
            {
                return Repository.GetProducts().ToList();
            }
            catch (Exception e)
            {
                Logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get product by name.
        /// </summary>
        public List<Product> GetProducts(string name)
        {
            try
            {
                return Repository.GetProducts(name).ToList();
            }
            catch (Exception e)
            {
                Logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

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
            catch (Exception e)
            {
                Logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Search product by name.
        /// </summary>
        public List<Product> SearchProducts(string name)
        {
            try
            {
                return Repository.GetProducts(name, true).ToList();
            }
            catch (Exception e)
            {
                Logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        public Product UpdateProduct(int id, string name, string description = "")
        {
            try
            {
                var product = ProductFactory.CreateProduct(name, description, id);

                Repository.UpdateProduct(product);

                return Repository.GetProduct(product.Id);
            }
            catch (Exception e)
            {
                Logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }
    }
}