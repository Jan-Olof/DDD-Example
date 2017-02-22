using System.Collections.Generic;

namespace ApplicationLayer.Interfaces
{
    public interface IBaseService<T>
    {
        T Create(T entity);

        IList<T> Get();

        T Get(int id);
    }
}