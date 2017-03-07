using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationLayer.Interfaces
{
    /// <summary>
    /// The generic repository interface. Handles CRUD operations.
    /// </summary>
    public interface IRepository<T> : IDisposable
    {
        /// <summary>
        /// Delete an entity object.
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Get all entity objects.
        /// </summary>
        IEnumerable<T> Get();

        /// <summary>
        /// Get entity objects based on a condition.
        /// </summary>
        IEnumerable<T> Get(Expression<Func<T, bool>> condition);

        /// <summary>
        /// Insert an entity object.
        /// </summary>
        T Insert(T entity);

        /// <summary>
        /// Update an entity object. This is based on a condition defining how to find the object.
        /// </summary>
        void Update(T entity, Expression<Func<T, bool>> findWhatToUpdate);
    }
}