using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Interfaces.Infrastructure;
using ApplicationLayer.Interfaces.Interactors;
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
        /// Create a new product.
        /// </summary>
        public Product Create(Product product)
        {
            try
            {
                var insertedProduct = _repository.InsertProduct(product);

                return insertedProduct;
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
        /// Get all products.
        /// </summary>
        public IList<Product> Get()
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
        /// Get product by id.
        /// </summary>
        public Product Get(int id)
        {
            try
            {
                return _repository.GetProduct(id);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(ProductEventId(), e, e.Message);
                throw new TooManyFoundException(e.Message, e);
            }
        }

        /// <summary>
        /// Get product by name.
        /// </summary>
        public IList<Product> Get(string name)
        {
            try
            {
                return _repository.GetProducts(_model.Get(name)).ToList();
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
        public IList<Product> Search(string name)
        {
            try
            {
                return _repository.GetProducts(_model.Search(name)).ToList();
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
        public Product Update(Product product)
        {
            try
            {
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