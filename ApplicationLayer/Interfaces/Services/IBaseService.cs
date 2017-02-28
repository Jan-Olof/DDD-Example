using System.Collections.Generic;

namespace ApplicationLayer.Interfaces.Services
{
    public interface IBaseService<T>
    {
        T Create(T entity);

        IList<T> Get();

        T Get(int id);
    }
}