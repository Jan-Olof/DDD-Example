using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Models;
using ApplicationLayer.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Utilities.Enums;
using Utilities.Exceptions;
using ApplicationLayer.Interfaces.Infrastructure;

namespace ApplicationLayer.Services
{
    /// <summary>
    /// The base service abstract class.
    /// </summary>
    public abstract class BaseService<T, TModel> : IBaseService<T> where T : class, IIdentifier where TModel : class, IModel<T>
    {
        protected readonly ILogger Logger;
        protected readonly TModel Model;
        protected readonly IRepository<T> Repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{T, TModel}"/> class.
        /// </summary>
        protected BaseService(IRepository<T> repository, TModel model, ILogger<BaseService<T, TModel>> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        /// <summary>
        /// Create a new entity object.
        /// </summary>
        public T Create(T entity)
        {
            try
            {
                return Repository.Insert(entity);
            }
            catch (Exception e)
            {
                Logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get all entity objects.
        /// </summary>
        public IList<T> Get()
        {
            try
            {
                return Repository.Get().ToList();
            }
            catch (Exception e)
            {
                Logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        public T Get(int id)
        {
            try
            {
                return Repository.Get(Model.Get(id)).SingleOrDefault();
            }
            catch (InvalidOperationException e)
            {
                Logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw new TooManyFoundException(e.Message, e);
            }
        }
    }
}