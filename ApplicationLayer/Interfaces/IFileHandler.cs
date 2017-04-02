using System.Collections.Generic;

namespace ApplicationLayer.Interfaces
{
    /// <summary>
    /// The file handler interface.
    /// </summary>
    public interface IFileHandler<T>
    {
        /// <summary>
        /// Get the file as an object of type T.
        /// </summary>
        T Get();

        /// <summary>
        /// Get all objects of this type.
        /// </summary>
        IList<T> GetList();
    }
}