using System;
using System.Collections.Generic;
using DomainLayer.Models;

namespace ApplicationLayer.Interfaces.Interactors
{
    /// <summary>
    /// The product interactor interface.
    /// </summary>
    public interface IProductInteractor : IDisposable
    {
        /// <summary>
        /// Create a new product.
        /// </summary>
        Product Create(string name, string description = "");

        /// <summary>
        /// Delete a product.
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// Get product by name.
        /// </summary>
        IList<Product> Get(string name);

        /// <summary>
        /// Get all products.
        /// </summary>
        IList<Product> Get();

        /// <summary>
        /// Get product by id.
        /// </summary>
        Product Get(int id);

        /// <summary>
        /// Search product by name.
        /// </summary>
        IList<Product> Search(string name);

        /// <summary>
        /// Update a product.
        /// </summary>
        Product Update(int id, string name, string description = "");
    }
}