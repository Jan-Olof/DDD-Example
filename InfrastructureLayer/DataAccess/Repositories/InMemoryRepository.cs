using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Interfaces.Models;

namespace InfrastructureLayer.DataAccess.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class, IIdentifier
    {
        private readonly IList<T> _entities;
        private readonly IUpdateMapper<T> _updateMapper;

        public InMemoryRepository(IUpdateMapper<T> updateMapper)
        {
            if (updateMapper == null)
            {
                throw new ArgumentNullException(nameof(updateMapper));
            }

            _entities = new List<T>();
            _updateMapper = updateMapper;
        }

        public InMemoryRepository(IUpdateMapper<T> updateMapper, IList<T> entities)
        {
            _updateMapper = updateMapper;
            _entities = entities;
        }

        public void Delete(T entity)
        {
            var item = _entities.SingleOrDefault(e => e.Id == entity.Id);

            int index = _entities.IndexOf(item);
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

        public void Update(T entity, Expression<Func<T, bool>> condition)
        {
            var toUpdate = _entities.SingleOrDefault(condition.Compile());

            _updateMapper.MapUpdate(entity, toUpdate);
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