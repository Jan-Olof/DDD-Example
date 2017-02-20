using System.Collections.Generic;

namespace ApplicationLayer.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
    }
}