using ApplicationLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationLayer.Interfaces.Infrastructure
{
    /// <summary>
    /// The domain repository interface. Handles CRUD operations.
    /// </summary>
    public interface IDomainRepository : IDisposable // TODO: Remove again?
    {
        /// <summary>
        /// Delete an entity object.
        /// </summary>
        void Delete(IInstruction entity);

        /// <summary>
        /// Fill the data set with data from the data store.
        /// </summary>
        void FillDataSet();

        /// <summary>
        /// Get all entity objects.
        /// </summary>
        IEnumerable<IInstruction> Get();

        /// <summary>
        /// Get entity objects based on a condition.
        /// </summary>
        IEnumerable<IInstruction> Get(Expression<Func<IInstruction, bool>> condition);

        /// <summary>
        /// Insert an entity object.
        /// </summary>
        IInstruction Insert(IInstruction entity);

        /// <summary>
        /// Persist data to the data store.
        /// </summary>
        void PersistData();

        /// <summary>
        /// Update an entity object. This is based on a condition defining how to find the object.
        /// </summary>
        void Update(IInstruction entity, Expression<Func<IInstruction, bool>> findWhatToUpdate);
    }
}