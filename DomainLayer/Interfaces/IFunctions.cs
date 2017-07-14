using System;
using System.Linq.Expressions;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// A generic interface for model functions.
    /// </summary>
    public interface IFunctions<T> : IUpdateMapper<T> where T : IIdentifier, IName
    {
        /// <summary>
        /// Defines how to get objects by id.
        /// </summary>
        Expression<Func<T, bool>> Get(int id);

        /// <summary>
        /// Defines how to get objects by name.
        /// </summary>
        Expression<Func<T, bool>> Get(string name);
    }
}