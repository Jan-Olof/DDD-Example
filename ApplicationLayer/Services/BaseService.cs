using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces;
using Utilities.Exceptions;

namespace ApplicationLayer.Services
{
    public abstract class BaseService<T, TModel> : IBaseService<T> where T : class, IDto where TModel : IModel<T>
    {
        protected readonly TModel Model;
        protected readonly IRepository<T> Repository;

        protected BaseService(IRepository<T> repository, TModel model)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Repository = repository;
            Model = model;
        }

        public T Create(T entity)
        {
            return Repository.Insert(entity);
        }

        public IList<T> Get()
        {
            return Repository.Get().ToList();
        }

        public T Get(int id)
        {
            try
            {
                return Repository.Get(Model.Get(id)).SingleOrDefault();
            }
            catch (InvalidOperationException e)
            {
                throw new TooManyFoundException(e.Message, e);
            }
        }
    }
}