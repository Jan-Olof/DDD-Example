using System;
using System.Linq.Expressions;

namespace ApplicationLayer.Interfaces.Models
{
    public interface IModel<T> : IUpdateMapper<T> where T : IIdentifier
    {
        Expression<Func<T, bool>> Get(int id);
    }
}