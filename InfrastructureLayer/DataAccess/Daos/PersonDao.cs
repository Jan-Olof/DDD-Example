using System.Collections.Generic;
using DomainLayer.Interfaces;
using DomainLayer.Models;

namespace InfrastructureLayer.DataAccess.Daos
{
    /// <summary>
    /// This is the person data access model.
    /// </summary>
    public class PersonDao : PersonBase<PersonDao>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDao"/> class.
        /// </summary>
        public PersonDao()
        {
            ProductPersons = new List<ProductPerson>();
        }

        /// <summary>
        /// Gets or sets the ProductPersons.
        /// </summary>
        public List<ProductPerson> ProductPersons { get; set; }
    }
}