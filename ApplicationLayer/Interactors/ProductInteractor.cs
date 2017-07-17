using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Infrastructure;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using Utilities.Enums;
using Utilities.Exceptions;

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
                return _repository.Insert(product);
            }
            catch (Exception e)
            {
                _logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        public IList<Product> Get()
        {
            try
            {
                return _repository.Get().ToList();
            }
            catch (Exception e)
            {
                _logger.LogError((int)LoggingEvents.Error, e, e.Message);
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
                return _repository.Get(_model.Get(id)).SingleOrDefault();
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError((int)LoggingEvents.Error, e, e.Message);
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
                return _repository.Get(_model.Get(name)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        public void Update(Product product, int id)
        {
            try
            {
                _repository.Update(product);
            }
            catch (Exception e)
            {
                _logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw;
            }
        }
    }
}