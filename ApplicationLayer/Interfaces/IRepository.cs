using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationLayer.Interfaces
{
    public interface IRepository<T>
    {
        void Delete(T entity);

        IEnumerable<T> Get();

        IEnumerable<T> Get(Expression<Func<T, bool>> condition);

        T Insert(T entity);

        void Update(T entity, Expression<Func<T, bool>> findWhatToUpdate);
    }
}