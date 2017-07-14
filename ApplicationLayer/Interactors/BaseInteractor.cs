using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Infrastructure;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Interfaces;
using Microsoft.Extensions.Logging;
using Utilities.Enums;
using Utilities.Exceptions;

namespace ApplicationLayer.Interactors
{
    /// <summary>
    /// The base interactor abstract class.
    /// </summary>
    public abstract class BaseInteractor<T, TModel> : IBaseInteractor<T> where T : class, IIdentifier, IName where TModel : class, IFunctions<T>
    {
        protected readonly ILogger Logger;
        protected readonly TModel Model;
        protected readonly IRepository<T> Repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseInteractor{T,TModel}"/> class.
        /// </summary>
        protected BaseInteractor(IRepository<T> repository, TModel model, ILogger<BaseInteractor<T, TModel>> logger)
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