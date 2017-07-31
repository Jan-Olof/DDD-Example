using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The product properties interface.
    /// </summary>
    public interface IProductDto : IProductBaseDto
    {
        /// <summary>
        /// Gets or sets the Persons.
        /// </summary>
        List<PersonInProduct> Persons { get; set; }
    }
}