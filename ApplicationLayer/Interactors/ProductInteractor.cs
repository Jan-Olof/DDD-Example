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
    public class ProductInteractor : IProductInteractor
    {
        private readonly ILogger _logger;
        private readonly IProduct _model;
        private readonly IDomainRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductInteractor"/> class.
        /// </summary>
        public ProductInteractor(IDomainRepository repository, IProduct model, ILogger<ProductInteractor> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        /// <summary>
        /// Add a person to a product.
        /// </summary>
        public void AddPersonToProduct(int personId, int productId, Role role)
        {
            try
            {
                var person = _repository.GetPerson(personId);

                if (person == null)
                {
                    throw new NotFoundException($"There is no person with id {personId}.");
                }

                var product = _repository.GetProduct(productId);

                if (product == null)
                {
                    throw new NotFoundException($"There is no product with id {productId}.");
                }

                if (product.Persons.SingleOrDefault(p => p.Id == personId && p.Role == role) != null)
                {
                    throw new TooManyFoundException(
                        $"There is already a person with id {personId} and role {role} in product {productId}.");
                }

                product.Persons.Add(PersonFactory.CreatePersonInProduct(personId, person.Name, role));

                _repository.UpdateProduct(product);
            }
            catch (Exception ex) when (ex is NotFoundException || ex is TooManyFoundException)
            {
                _logger.LogWarning($"AddPersonToProduct failed: {ex.Message}");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(ProductEventId(), e, e.Message);
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

                var insertedProduct = _repository.AddProduct(product);

                return insertedProduct;
            }
            catch (Exception e)
            {
                _logger.LogError(ProductEventId(), e, e.Message);
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
                _repository.RemoveProduct(id);
            }
            catch (Exception e)
            {
                _logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _repository?.Dispose();
        }

        /// <summary>
        /// Get product by id.
        /// </summary>
        public Product GetProduct(int id)
        {
            try
            {
                return _repository.GetProduct(id);
            }
            catch (Exception e)
            {
                _logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        public IList<Product> GetProducts()
        {
            try
            {
                return _repository.GetProducts().ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get product by name.
        /// </summary>
        public IList<Product> GetProducts(string name)
        {
            try
            {
                return _repository.GetProducts(name).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(ProductEventId(), e, e.Message);
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
                var product = _repository.GetProduct(productId);

                if (product == null)
                {
                    throw new NotFoundException($"There is no product with id {productId}.");
                }

                var personInProduct = product.Persons.SingleOrDefault(p => p.Id == personId && p.Role == role);

                if (personInProduct == null)
                {
                    throw new NotFoundException($"There is no person with id {personId} and role {role} in product {productId}.");
                }

                product.Persons.Remove(personInProduct);

                _repository.UpdateProduct(product);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning($"RemovePersonFromProduct failed: {ex.Message}");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Search product by name.
        /// </summary>
        public IList<Product> SearchProducts(string name)
        {
            try
            {
                return _repository.GetProducts(name, true).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(ProductEventId(), e, e.Message);
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

                _repository.UpdateProduct(product);

                return _repository.GetProduct(product.Id);
            }
            catch (Exception e)
            {
                _logger.LogError(ProductEventId(), e, e.Message);
                throw;
            }
        }
    }
}