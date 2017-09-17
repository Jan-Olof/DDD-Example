using DomainLayer.Exceptions;
using DomainLayer.Interfaces;
using InfrastructureLayer.EventLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace InfrastructureLayer.DataAccess.Repositories
{
    /// <summary>
    /// The entity framwork base implementation of a repository interface.
    /// </summary>
    public abstract class EfRepositoryBase : IDisposable
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly DbContext _context;

        /// <summary>
        /// The logging interface.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfRepositoryBase"/> class.
        /// </summary>
        protected EfRepositoryBase(DbContext dataContext, ILogger logger)
        {
            _context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
            => _context?.Dispose();

        /// <summary>
        /// Delete an entity.
        /// </summary>
        protected void Delete<T>(int id) where T : class, IIdentifier
        {
            var entity = FindEntity<T>(id);

            _context.Remove(entity);

            SaveChanges(entity, EventType.Delete);

            _logger.LogInformation(JsonConvert.SerializeObject(EventObjectFactory<T>.CreateEventObject(entity, EventType.Delete), IgnoreReferenced()));
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        protected void Delete<T>(Expression<Func<T, bool>> condition) where T : class, IIdentifier
        {
            try
            {
                var entity = _context.Set<T>().SingleOrDefault(condition);

                if (entity == null)
                {
                    throw new NotFoundException("Object to delete not found.");
                }

                _context.Remove(entity);

                SaveChanges(entity, EventType.Delete);

                _logger.LogInformation(JsonConvert.SerializeObject(EventObjectFactory<T>.CreateEventObject(entity, EventType.Delete), IgnoreReferenced()));
            }
            catch (InvalidOperationException ex)
            {
                Helpers.Extensions.LoggerExtensions.LogErrorWithInnerExceptions(_logger, ex);
                throw new TooManyFoundException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Insert a new entity.
        /// </summary>
        protected T Insert<T>(T entity) where T : class, IIdentifier
        {
            _context.Add(entity);

            SaveChanges(entity, EventType.Create);

            _logger.LogInformation(JsonConvert.SerializeObject(EventObjectFactory<T>.CreateEventObject(entity, EventType.Create), IgnoreReferenced()));

            return entity;
        }

        /// <summary>
        /// Save change, handle exceptions that it might cause and log the event.
        /// </summary>
        protected void SaveChanges<T>(T entity, EventType eventType) where T : class, IIdentifier
        {
            try
            {
                int changes = _context.SaveChanges();

                if (changes == 0)
                {
                    throw new NoChangesException($"No changes in context after SaveChanges when doing {eventType}. Type: {typeof(T)} Id: {entity.Id}.");
                }

                _logger.LogInformation(JsonConvert.SerializeObject(EventObjectFactory<T>.CreateEventObject(entity, eventType), IgnoreReferenced()));
            }
            catch (DbUpdateException ex)
            {
                Helpers.Extensions.LoggerExtensions.LogErrorWithInnerExceptions(_logger, ex);
                throw;
            }
        }

        /// <summary>
        /// Update an entity object, and maybe save changes in the database.
        /// </summary>
        protected T Update<T>(T entity, bool saveChanges = false) where T : class, IIdentifier, IUpdateMapper<T>
        {
            var entityToUpdate = FindEntity<T>(entity.Id);

            entity.MapUpdate(entity, entityToUpdate);

            if (saveChanges)
            {
                SaveChanges(entityToUpdate, EventType.Update);
            }

            return entityToUpdate;
        }

        /// <summary>
        /// Update an entity object, and maybe save changes in the database.
        /// </summary>
        protected T Update<T>(T entity, Func<IQueryable<T>, IQueryable<T>> includeMembers, bool saveChanges = false) where T : class, IIdentifier, IUpdateMapper<T>
        {
            var entityToUpdate = FindEntity(entity.Id, includeMembers);

            entity.MapUpdate(entity, entityToUpdate);

            if (saveChanges)
            {
                SaveChanges(entityToUpdate, EventType.Update);
            }

            return entityToUpdate;
        }

        /// <summary>
        /// Set JSON serielizer to ignore referenced object.
        /// </summary>
        private static JsonSerializerSettings IgnoreReferenced()
            => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

        /// <summary>
        /// Find an entity and handle null exception.
        /// </summary>
        private T FindEntity<T>(int id) where T : class
        {
            var entity = _context.Set<T>().Find(id);

            if (entity == null)
            {
                throw new NotFoundException($"Could not find an object with id {id}.");
            }

            return entity;
        }

        /// <summary>
        /// Find an entity and handle null exception.
        /// </summary>
        private T FindEntity<T>(int id, Func<IQueryable<T>, IQueryable<T>> includeMembers) where T : class, IIdentifier
        {
            try
            {
                var entity =
                    includeMembers(_context.Set<T>())
                    .SingleOrDefault(arg => arg.Id == id);

                if (entity == null)
                {
                    throw new NotFoundException($"Could not find an object with id {id}.");
                }

                return entity;
            }
            catch (InvalidOperationException ex)
            {
                Helpers.Extensions.LoggerExtensions.LogErrorWithInnerExceptions(_logger, ex);
                throw new TooManyFoundException(ex.Message, ex);
            }
        }
    }
}