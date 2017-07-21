using System.Collections.Generic;
using DomainLayer.Models;

namespace ApplicationLayer.Interfaces.Interactors
{
    /// <summary>
    /// The product interactor interface.
    /// </summary>
    public interface IProductInteractor
    {
        /// <summary>
        /// Create a new product.
        /// </summary>
        Product Create(Product product);

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
        /// Update a product.
        /// </summary>
        void Update(Product product);
    }
}