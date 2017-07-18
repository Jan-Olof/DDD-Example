using System;
using System.Linq.Expressions;
using DomainLayer.Enums;
using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    public interface IProductPersonFunctions
    {
        /// <summary>
        /// Defines how to get objects by id.
        /// </summary>
        Expression<Func<ProductPerson, bool>> Get(int productId, int personId, Role role);
    }
}