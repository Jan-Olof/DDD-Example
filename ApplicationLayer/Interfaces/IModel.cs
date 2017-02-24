using System;
using System.Linq.Expressions;

namespace ApplicationLayer.Interfaces
{
    public interface IModel<T> where T : IDto
    {
        Expression<Func<T, bool>> Get(int id);
    }
}