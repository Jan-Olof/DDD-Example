using System;
using System.Linq.Expressions;

namespace ApplicationLayer.Interfaces.Models
{
    /// <summary>
    /// A generic model interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IModel<T> : IUpdateMapper<T> where T : IIdentifier
    {
        /// <summary>
        /// Defines how to get instructions by id.
        /// </summary>
        Expression<Func<T, bool>> Get(int id);
    }
}