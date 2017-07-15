using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The person properties interface.
    /// </summary>
    public interface IPersonProps : IIdentifier, IName
    {
        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the ProductPersons.
        /// </summary>
        List<ProductPerson> ProductPerson { get; set; }
    }
}