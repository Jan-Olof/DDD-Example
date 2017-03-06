using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Interfaces.Models;
using ApplicationLayer.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Utilities.Enums;
using Utilities.Exceptions;

namespace ApplicationLayer.Services
{
    public abstract class BaseService<T, TModel> : IBaseService<T> where T : class, IIdentifier where TModel : IModel<T>
    {
        protected readonly ILogger Logger;
        protected readonly TModel Model;
        protected readonly IRepository<T> Repository;

        protected BaseService(IRepository<T> repository, TModel model, ILogger<BaseService<T, TModel>> logger)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            Logger = logger;
            Repository = repository;
            Model = model;
        }

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