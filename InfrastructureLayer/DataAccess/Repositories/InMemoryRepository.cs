using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApplicationLayer.Interfaces;

namespace InfrastructureLayer.DataAccess.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class, IDto
    {
        private readonly IList<T> _entities;

        public InMemoryRepository()
        {
            _entities = new List<T>();
        }

        public InMemoryRepository(IList<T> entities)
        {
            _entities = entities;
        }

        public void Delete(T entity)
        {
            int index = _entities.IndexOf(entity);
            _entities.RemoveAt(index);
        }

        public IEnumerable<T> Get()
        {
            return _entities;
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> condition)
        {
            return _entities.Where(condition.Compile());
        }

        public T Insert(T entity)
        {
            entity.Id = GetNextId();
            _entities.Add(entity);
            return entity;
        }

        private int GetNextId()
        {
            if (_entities?.Any() == false)
            {
                return 1;
            }

            return _entities.Max(n => n.Id) + 1;
        }
    }
}