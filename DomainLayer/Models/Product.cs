﻿using DomainLayer.Interfaces;
using System.Collections.Generic;

namespace DomainLayer.Models
{
    /// <summary>
    /// This is the product domain model.
    /// </summary>
    public class Product : ProductBase<Product>, IProduct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            Name = string.Empty;
            Description = string.Empty;
            Persons = new List<PersonInProduct>();
        }

        /// <summary>
        /// Gets or sets the persons belonging to the product.
        /// </summary>
        public List<PersonInProduct> Persons { get; set; }
    }
}