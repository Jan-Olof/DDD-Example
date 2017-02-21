using System;
using System.Linq.Expressions;

namespace Utilities.Interfaces
{
    public interface IModel<T> where T : IDto
    {
        Expression<Func<T, bool>> Get(int id);
    }
}