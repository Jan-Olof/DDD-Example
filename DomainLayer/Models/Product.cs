using System.Collections.Generic;
using DomainLayer.Interfaces;

namespace DomainLayer.Models
{
    /// <summary>
    /// This is the product domain model.
    /// </summary>
    public class Product : ProductBase<Product>, IProduct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            Name = string.Empty;
            Description = string.Empty;
            Persons = new List<PersonInProduct>();
        }

        /// <summary>
        /// Gets or sets the name. This is the name of the entity.
        /// </summary>
        public sealed override string Name { get; set; }

        /// <summary>
        /// Gets or sets the persons belonging to the product.
        /// </summary>
        public IList<PersonInProduct> Persons { get; set; }
    }
}