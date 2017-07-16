﻿using System;
using System.Linq;
using System.Linq.Expressions;
using DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utilities.Enums;
using Utilities.Exceptions;

namespace InfrastructureLayer.DataAccess.Repositories
{
    /// <summary>
    /// The entity framwork base implementation of a repository interface.
    /// </summary>
    public abstract class RepositoryEfBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        protected readonly DbContext Context;

        /// <summary>
        /// The logging interface.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryEfBase"/> class.
        /// </summary>
        protected RepositoryEfBase(DbContext dataContext, ILogger logger)
        {
            Context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        protected void Delete<T>(int id) where T : class
        {
            var entity = FindEntity<T>(id);

            Context.Remove(entity);

            SaveChanges($"No changes in context when deleting object with id {id}.");
        }

        /// <summary>
        /// Get all entities of a certain type.
        /// </summary>
        protected IQueryable<T> Get<T>() where T : class
        {
            return Context.Set<T>();
        }

        /// <summary>
        /// Get all entities of a certain type based on a condition.
        /// </summary>
        protected IQueryable<T> Get<T>(Expression<Func<T, bool>> condition) where T : class
        {
            return Context.Set<T>().Where(condition);
        }

        /// <summary>
        /// Insert a new entity.
        /// </summary>
        protected T Insert<T>(T entity) where T : class
        {
            Context.Add(entity);

            SaveChanges("No changes in context when adding new object.");

            return entity;
        }

        /// <summary>
        /// Update an entity object. This is based on a condition defining how to find the object and a update mapping.
        /// </summary>
        protected void Update<T>(T entity) where T : class, IIdentifier, IUpdateMapper<T>
        {
            var entityToUpdate = FindEntity<T>(entity.Id);

            entity.MapUpdate(entity, entityToUpdate);

            SaveChanges($"No changes in context after SaveChanges when updating id {entity.Id}.");
        }

        /// <summary>
        /// Find an entity and handle null exception.
        /// </summary>
        private T FindEntity<T>(int id) where T : class
        {
            var entityToUpdate = Context.Set<T>().Find(id);

            if (entityToUpdate == null)
            {
                throw new NotFoundException($"Could not find an object with id {id}.");
            }

            return entityToUpdate;
        }

        /// <summary>
        /// Log an exception and include an inner exception.
        /// </summary>
        private void LogExceptionWithInnerException(Exception ex)
        {
            _logger.LogError((int)LoggingEvents.Error, ex, ex.Message);

            if (ex.InnerException != null)
            {
                _logger.LogError((int)LoggingEvents.Error, ex.InnerException, $"Inner exception message: {ex.InnerException.Message}");
            }
        }

        /// <summary>
        /// Save change and handle exceptions that it might cause.
        /// </summary>
        private void SaveChanges(string noChangesExceptionMsg = "No changes in context after SaveChanges.")
        {
            try
            {
                int changes = Context.SaveChanges();

                if (changes == 0)
                {
                    throw new NoChangesException(noChangesExceptionMsg);
                }
            }
            catch (DbUpdateException ex)
            {
                LogExceptionWithInnerException(ex);
                throw;
            }
        }
    }
}