using System;
using System.Linq.Expressions;

namespace DomainLayer.Interfaces
{
    public interface IModel<T>
    {
        Expression<Func<T, bool>> Get(int id);
    }
}