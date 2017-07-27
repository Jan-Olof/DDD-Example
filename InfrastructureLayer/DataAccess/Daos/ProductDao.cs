using System.Collections.Generic;
using DomainLayer.Interfaces;
using DomainLayer.Models;

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
        {
            ProductPersons = new List<ProductPerson>();
        }

        public override string Name { get; set; }

        /// <summary>
        /// Gets or sets the ProductPersons.
        /// </summary>
        public List<ProductPerson> ProductPersons { get; set; }
    }
}