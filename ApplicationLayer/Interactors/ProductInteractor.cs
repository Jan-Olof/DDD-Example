using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Infrastructure;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using Utilities.Enums;

namespace ApplicationLayer.Interactors
{
    /// <summary>
    /// The product interactor class. Handles the stories/tasks concerning products.
    /// </summary>
    public class ProductInteractor : BaseInteractor<Product, IProduct>, IProductInteractor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductInteractor"/> class.
        /// </summary>
        public ProductInteractor(IRepository<Product> repository, IProduct model, ILogger<ProductInteractor> logger)
            : base(repository, model, logger)
        {
        }

        /// <summary>
        /// Get product by name.
        /// </summary>
        public IList<Product> Get(string name)
        {
            try
            {
                return Repository.Get(Model.Get(name)).ToList();
            }
            catch (Exception e)
            {
                Logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        public void Update(Product entity, int id)
        {
            try
            {
                Repository.Update(entity, Model.Get(id));
            }
            catch (Exception e)
            {
                Logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw;
            }
        }
    }
}