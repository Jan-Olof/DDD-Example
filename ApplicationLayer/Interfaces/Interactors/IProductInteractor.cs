using System;
using System.Collections.Generic;
using DomainLayer.Enums;
using DomainLayer.Models;

namespace ApplicationLayer.Interfaces.Interactors
{
    /// <summary>
    /// The product interactor interface.
    /// </summary>
    public interface IProductInteractor : IDisposable
    {
        /// <summary>
        /// Add a person to a product.
        /// </summary>
        void AddPersonToProduct(int personId, int productId, Role role);

        /// <summary>
        /// Create a new product.
        /// </summary>
        Product CreateProduct(string name, string description = "");

        /// <summary>
        /// Delete a product.
        /// </summary>
        void DeleteProduct(int id);

        /// <summary>
        /// Get product by id.
        /// </summary>
        Product GetProduct(int id);

        /// <summary>
        /// Get product by name.
        /// </summary>
        List<Product> GetProducts(string name);

        /// <summary>
        /// Get all products.
        /// </summary>
        List<Product> GetProducts();

        /// <summary>
        /// Remove a person from a product.
        /// </summary>
        void RemovePersonFromProduct(int personId, int productId, Role role);

        /// <summary>
        /// Search product by name.
        /// </summary>
        List<Product> SearchProducts(string name);

        /// <summary>
        /// Update a product.
        /// </summary>
        Product UpdateProduct(int id, string name, string description = "");
    }
}