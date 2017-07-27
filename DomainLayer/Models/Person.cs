using System;
using System.Collections.Generic;
using DomainLayer.Interfaces;

namespace DomainLayer.Models
{
    /// <summary>
    /// This is the person domain model.
    /// </summary>
    public class Person : PersonBase<Person>, IPerson
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Products = new List<Product>();
        }

        /// <summary>
        /// Gets or sets the products that belong to a person.
        /// </summary>
        public IList<Product> Products { get; set; }
    }
}