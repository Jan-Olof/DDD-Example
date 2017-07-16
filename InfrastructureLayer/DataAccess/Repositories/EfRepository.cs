using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Interfaces;

namespace InfrastructureLayer.DataAccess.Repositories
{
    // TODO: This will have to change to a DomainRepository once we add another aggregate. (It can implement both interfaces.)

    /// <summary>
    /// The entity framwork implementation of the generic repository.
    /// </summary>
    public class EfRepository<T> : RepositoryEfBase, IRepository<T> where T : class, IUpdateMapper<T>, IIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EfRepository{T}"/> class.
        /// </summary>
        public EfRepository(DbContext dataContext, ILogger<EfRepository<T>> logger)
            : base(dataContext, logger)
        {
        }

        /// <summary>
        /// Delete an entity object.
        /// </summary>
        public void Delete(T entity)
        {
            base.Delete(entity);
        }

        /// <summary>
        /// Fill the data set with data from the data store.
        /// EF don't support this behaviour.
        /// </summary>
        public void FillDataSet()
        {
        }

        /// <summary>
        /// Get all entity objects.
        /// </summary>
        public IEnumerable<T> Get()
        {
            return Get<T>();
        }

        /// <summary>
        /// Get entity objects based on a condition.
        /// </summary>
        public IEnumerable<T> Get(Expression<Func<T, bool>> condition)
        {
            return base.Get(condition);
        }

        /// <summary>
        /// Insert an entity object.
        /// </summary>
        public T Insert(T entity)
        {
            return base.Insert(entity);
        }

        /// <summary>
        /// Persist data to the data store.
        /// EF don't support this behaviour.
        /// </summary>
        public void PersistData()
        {
        }

        /// <summary>
        /// Update an entity object. This is based on a condition defining how to find the object.
        /// </summary>
        public void Update(T entity)
        {
            base.Update(entity);
        }
    }
}