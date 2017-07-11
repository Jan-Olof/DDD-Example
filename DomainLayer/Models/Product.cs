using System;
using System.Linq.Expressions;
using DomainLayer.Interfaces;

namespace DomainLayer.Models
{
    /// <summary>
    /// This is the product domain model.
    /// </summary>
    public class Product : IProduct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            Description = string.Empty;
            Name = string.Empty;
        }

        /// <summary>
        /// Gets or sets the description. A text field that describes the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id. The primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name. This is the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Defines how to get products by id.
        /// </summary>
        public Expression<Func<Product, bool>> Get(int id)
        {
            return product => product.Id == id;
        }

        /// <summary>
        /// Defines how to get products by name.
        /// </summary>
        public Expression<Func<Product, bool>> Get(string name)
        {
            return product => product.Name == name;
        }

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