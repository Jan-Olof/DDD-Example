using DomainLayer.Enums;
using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    public interface IProductPersonProps
    {
        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        Person Person { get; set; }

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        Product Product { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        Role Role { get; set; }
    }
}