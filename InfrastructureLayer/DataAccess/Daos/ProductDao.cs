using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfrastructureLayer.DataAccess.Daos
{
    /// <summary>
    /// This is the product data access model.
    /// </summary>
    public class ProductDao : ProductBase<ProductDao>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDao"/> class.
        /// </summary>
        public ProductDao()
            => ProductPersons = new List<ProductPerson>();

        /// <summary>
        /// Gets or sets the ProductPersons.
        /// </summary>
        public List<ProductPerson> ProductPersons { get; set; }

        /// <summary>
        /// Members to include for ProductDao. (ProductPersons and then Person.)
        /// </summary>
        public static Func<IQueryable<ProductDao>, IQueryable<ProductDao>> IncludeMembers()
            => productDaos => productDaos
                .Include(productDao => productDao.ProductPersons)
                .ThenInclude(productPerson => productPerson.Person);
    }
}