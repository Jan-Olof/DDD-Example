using System.Collections.Generic;
using DomainLayer.Models;

namespace ApplicationLayer.Interfaces.Interactors
{
    /// <summary>
    /// The product interactor interface.
    /// </summary>
    public interface IProductInteractor : IBaseInteractor<Product>
    {
        /// <summary>
        /// Get product by name.
        /// </summary>
        IList<Product> Get(string name);

        /// <summary>
        /// Update a product.
        /// </summary>
        void Update(Product entity, int id);
    }
}