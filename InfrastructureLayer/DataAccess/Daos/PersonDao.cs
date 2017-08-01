using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Members to include for PersonDao. (ProductPersons and then Product.)
        /// </summary>
        public static Func<IQueryable<PersonDao>, IQueryable<PersonDao>> IncludeMembers()
        {
            return personDaos => personDaos
                .Include(personDao => personDao.ProductPersons)
                .ThenInclude(productPerson => productPerson.Product);
        }
    }
}