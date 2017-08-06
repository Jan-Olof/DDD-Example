﻿using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The person properties interface.
    /// </summary>
    public interface IPersonDto : IIdentifier, IName, IPersonCreate
    {
        /// <summary>
        /// Gets or sets the products that belong to a person.
        /// </summary>
        IList<ProductInPerson> Products { get; set; }
    }
}