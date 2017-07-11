using System;
using System.Linq.Expressions;
using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The product model functions interface.
    /// </summary>
    public interface IProductFunctions : IFunctions<Product>
    {
        /// <summary>
        /// Defines how to get product by name.
        /// </summary>
        Expression<Func<Product, bool>> Get(string name);
    }
}