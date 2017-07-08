using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utilities.Expressions;
using Utilities.Enums;
using Utilities.Exceptions;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Interfaces;

namespace InfrastructureLayer.DataAccess.Repositories
{
    /// <summary>
    /// The entity framwork implementation of the generic repository.
    /// </summary>
    public class EfRepository<T, TModel> : IRepository<T> where T : class where TModel : class, T
    {
        private readonly DbContext _context;
        private readonly ILogger _logger;
        private readonly IUpdateMapper<T> _updateMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfRepository{T, TModel}"/> class.
        /// </summary>
        public EfRepository(DbContext dataContext, IUpdateMapper<T> updateMapper, ILogger<EfRepository<T, TModel>> logger)
        {
            _context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _updateMapper = updateMapper ?? throw new ArgumentNullException(nameof(updateMapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Delete an entity object.
        /// </summary>
        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
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
            return _context.Set<TModel>();
        }

        /// <summary>
        /// Get entity objects based on a condition.
        /// </summary>
        public IEnumerable<T> Get(Expression<Func<T, bool>> condition)
        {
            var expression = ChangeType.ChangeInputType<T, TModel, bool>(condition);

            return _context.Set<TModel>().Where(expression);
        }

        /// <summary>
        /// Insert an entity object.
        /// </summary>
        public T Insert(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();

            return entity;
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
        public void Update(T entity, Expression<Func<T, bool>> condition)
        {
            try
            {
                var toUpdate = _context.Set<TModel>().SingleOrDefault(condition);

                if (toUpdate == null)
                {
                    throw new NullReferenceException("No value was found for toUpdate.");
                }

                _updateMapper.MapUpdate(entity, toUpdate);

                int changes = _context.SaveChanges();
                _logger.LogInformation($"Saved {changes} changes.");
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw new TooManyFoundException(e.Message, e);
            }
        }
    }
}