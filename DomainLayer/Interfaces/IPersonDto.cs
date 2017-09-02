using DomainLayer.Models;
using System.Collections.Generic;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The person properties interface.
    /// </summary>
    public interface IPersonDto : IName, IPersonUpdate
    {
        /// <summary>
        /// Gets or sets the products that belong to a person.
        /// </summary>
        IList<ProductInPerson> Products { get; set; }
    }
}