using InfrastructureLayer.Configure;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using ApplicationLayer.Interfaces.Infrastructure;

namespace InfrastructureLayer.Files
{
    /// <summary>
    /// The file handler class.
    /// </summary>
    /// <typeparam name="T">The type to serialize to or from.</typeparam>
    public class FileHandler<T> : IFileHandler<T>
    {
        private readonly IOptions<Datafile> _datafile;
        private readonly IJsonSerialization _serialization;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileHandler{T}"/> class.
        /// </summary>
        public FileHandler(IOptions<Datafile> datafile, IJsonSerialization serialization)
        {
            _datafile = datafile ?? throw new ArgumentNullException(nameof(datafile));
            _serialization = serialization ?? throw new ArgumentNullException(nameof(serialization));
        }

        /// <summary>
        /// Get the file as an object of type T.
        /// </summary>
        public T Read()
        {
            var fileStream = new FileStream(_datafile.Value.FileName, FileMode.Open);

            return _serialization.Deserialize<T>(fileStream);
        }

        /// <summary>
        /// Write an object of type T to a file.
        /// </summary>
        public void Write(T obj)
        {
            File.WriteAllText(_datafile.Value.FileName, string.Empty);

            var fileStream = new FileStream(_datafile.Value.FileName, FileMode.Open);

            _serialization.Serialize(obj, fileStream);
        }
    }
}