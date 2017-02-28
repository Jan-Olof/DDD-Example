using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApplicationLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Utilities;

namespace InfrastructureLayer.DataAccess.Repositories
{
    public class EfRepository<T, TModel> : IRepository<T>, IDisposable where T : class where TModel : class, T
    {
        private readonly DbContext _context;

        public EfRepository(DbContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            _context = dataContext;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public IEnumerable<T> Get()
        {
            return _context.Set<TModel>();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> condition)
        {
            var expression = ChangeType.ChangeInputType<T, TModel, bool>(condition);

            return _context.Set<TModel>().Where(expression);
        }

        public T Insert(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}