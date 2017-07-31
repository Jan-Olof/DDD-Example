using System.Collections.Generic;
using DomainLayer.Interfaces;
using DomainLayer.Models;

namespace InfrastructureLayer.Dtos
{
    /// <summary>
    /// The product data transfer object.
    /// </summary>
    public class ProductDto : IProductDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDto"/> class.
        /// </summary>
        public ProductDto()
        {
            Description = string.Empty;
            Name = string.Empty;
            Persons = new List<PersonInProduct>();
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
        /// Gets or sets the persons.
        /// </summary>
        public List<PersonInProduct> Persons { get; set; }
    }
}