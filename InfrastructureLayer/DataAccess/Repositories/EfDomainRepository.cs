using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Models;

namespace InfrastructureLayer.DataAccess.Repositories
{
    /// <summary>
    /// The entity framwork implementation of the domain repository.
    /// </summary>
    public class EfDomainRepository : EfRepositoryBase, IDomainRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EfDomainRepository"/> class.
        /// </summary>
        public EfDomainRepository(DbContext dataContext, ILogger<EfDomainRepository> logger)
            : base(dataContext, logger)
        {
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void Delete(Product product)
        {
            base.Delete(product);
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void Delete(int id)
        {
            Delete<Product>(id);
        }

        /// <summary>
        /// Fill the data set with data from the data store.
        /// EF don't support this behaviour.
        /// </summary>
        public void FillDataSet()
        {
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        public IEnumerable<Product> Get()
        {
            return Get<Product>();
        }

        /// <summary>
        /// Get products based on a condition.
        /// </summary>
        public IEnumerable<Product> Get(Expression<Func<Product, bool>> condition)
        {
            return base.Get(condition);
        }

        /// <summary>
        /// Insert a product.
        /// </summary>
        public Product Insert(Product product)
        {
            return base.Insert(product);
        }

        /// <summary>
        /// Persist data to the data store.
        /// EF don't support this behaviour.
        /// </summary>
        public void PersistData()
        {
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        public void Update(Product product)
        {
            base.Update(product);
        }
    }
}