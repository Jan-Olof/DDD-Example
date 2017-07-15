using System.Collections.Generic;
using DomainLayer.Interfaces;

namespace DomainLayer.Models
{
    /// <summary>
    /// This is the product domain model.
    /// </summary>
    public sealed class Product : Entity<Product>, IProduct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the description. A text field that describes the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ProductPersons.
        /// </summary>
        public List<ProductPerson> ProductPerson { get; set; }

        /// <summary>
        /// Updates the fields that are supposed to be updated when editing a product.
        /// </summary>
        public Product MapUpdate(Product from, Product to)
        {
            to.Name = from.Name;
            to.Description = from.Description;

            return to;
        }
    }
}