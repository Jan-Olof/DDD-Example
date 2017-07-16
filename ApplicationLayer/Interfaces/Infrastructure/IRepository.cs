﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationLayer.Interfaces.Infrastructure
{
    /// <summary>
    /// The generic repository interface. Handles CRUD operations.
    /// </summary>
    public interface IRepository<T> : IDisposable // TODO: Decide if to remove generic repository interface and replace with a domain repository.
    {
        /// <summary>
        /// Delete an object.
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Fill the data set with data from the data store.
        /// </summary>
        void FillDataSet();

        /// <summary>
        /// Get all objects.
        /// </summary>
        IEnumerable<T> Get();

        /// <summary>
        /// Get objects based on a condition.
        /// </summary>
        IEnumerable<T> Get(Expression<Func<T, bool>> condition);

        /// <summary>
        /// Insert an object.
        /// </summary>
        T Insert(T entity);

        /// <summary>
        /// Persist data to the data store.
        /// </summary>
        void PersistData();

        /// <summary>
        /// Update an object. This is based on a condition defining how to find the object.
        /// </summary>
        void Update(T entity);
    }
}