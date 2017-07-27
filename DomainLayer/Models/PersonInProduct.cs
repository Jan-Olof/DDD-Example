using DomainLayer.Enums;
using DomainLayer.Interfaces;

namespace DomainLayer.Models
{
    /// <summary>
    /// A person that belongs to a product.
    /// </summary>
    public class PersonInProduct : IPersonProduct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonInProduct"/> class.
        /// </summary>
        public PersonInProduct()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Name = string.Empty;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

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