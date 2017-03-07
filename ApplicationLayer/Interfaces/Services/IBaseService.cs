using System.Collections.Generic;

namespace ApplicationLayer.Interfaces.Services
{
    /// <summary>
    /// The base service interface.
    /// </summary>
    public interface IBaseService<T>
    {
        /// <summary>
        /// Create a new entity object.
        /// </summary>
        T Create(T entity);

        /// <summary>
        /// Get all entity objects.
        /// </summary>
        IList<T> Get();

        /// <summary>
        /// Get entity by id.
        /// </summary>
        T Get(int id);
    }
}