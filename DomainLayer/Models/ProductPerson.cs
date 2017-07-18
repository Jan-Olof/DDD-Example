using System;
using System.Linq.Expressions;
using DomainLayer.Enums;
using DomainLayer.Interfaces;

namespace DomainLayer.Models
{
    /// <summary>
    /// Handle relations between product and person.
    /// </summary>
    public class ProductPerson : IProductPerson
    {
        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Defines how to get objects by id.
        /// </summary>
        public Expression<Func<ProductPerson, bool>> Get(int productId, int personId, Role role)
        {
            return p => p.ProductId == productId && p.PersonId == personId && p.Role == role;
        }
    }
}