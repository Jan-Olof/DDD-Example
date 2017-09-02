using DomainLayer.Interfaces;
using DomainLayer.Models;
using System.Collections.Generic;

namespace InfrastructureLayer.Dtos
{
    /// <summary>
    /// The person data transfer object.
    /// </summary>
    public class PersonDto : IPersonDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDto"/> class.
        /// </summary>
        public PersonDto()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Name = string.Empty;
            Products = new List<ProductInPerson>();
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
        /// Gets or sets the products.
        /// </summary>
        public IList<ProductInPerson> Products { get; set; }
    }
}