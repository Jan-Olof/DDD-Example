using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The product properties interface.
    /// </summary>
    public interface IProductProps : IIdentifier, IName
    {
        /// <summary>
        /// Gets or sets the description. A text field that describes the product.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the ProductPersons.
        /// </summary>
        List<ProductPerson> ProductPerson { get; set; }
    }
}