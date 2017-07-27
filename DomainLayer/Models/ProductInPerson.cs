using DomainLayer.Enums;
using DomainLayer.Interfaces;

namespace DomainLayer.Models
{
    /// <summary>
    /// A product that belongs to a person.
    /// </summary>
    public class ProductInPerson : IProductPerson
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductInPerson"/> class.
        /// </summary>
        public ProductInPerson()
        {
            Description = string.Empty;
            Name = string.Empty;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }
    }
}