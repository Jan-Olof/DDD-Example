using System.Collections.Generic;

namespace ApplicationLayer.Interfaces.Infrastructure
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
    }
}