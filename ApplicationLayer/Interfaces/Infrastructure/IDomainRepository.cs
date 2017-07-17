using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DomainLayer.Models;

namespace ApplicationLayer.Interfaces.Infrastructure
{
    /// <summary>
    /// The domain repository interface. Handles CRUD operations.
    /// </summary>
    public interface IDomainRepository : IDisposable
    {
        /// <summary>
        /// Delete a product.
        /// </summary>
        void DeleteProduct(Product product);

        /// <summary>
        /// Delete a product.
        /// </summary>
        void DeleteProduct(int id);

        /// <summary>
        /// Fill the data set with data from the data store.
        /// </summary>
        void FillDataSet();

        /// <summary>
        /// Get all products.
        /// </summary>
        IEnumerable<Product> GetProducts();

        /// <summary>
        /// Get products based on a condition.
        /// </summary>
        IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> condition);

        /// <summary>
        /// Insert a product.
        /// </summary>
        Product InsertProduct(Product product);

        /// <summary>
        /// Persist data to the data store.
        /// </summary>
        void PersistData();

        /// <summary>
        /// Update a product.
        /// </summary>
        void UpdateProduct(Product product);
    }
}