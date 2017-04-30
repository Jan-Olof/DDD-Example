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
        T Read();

        /// <summary>
        /// Write an object of type T to a file.
        /// </summary>
        void Write(T obj);
    }
}