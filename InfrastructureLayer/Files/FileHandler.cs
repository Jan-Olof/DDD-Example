using ApplicationLayer.Interfaces;
using InfrastructureLayer.Configure;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Utilities.Serialization;

namespace InfrastructureLayer.Files
{
    /// <summary>
    /// The file handler class.
    /// </summary>
    /// <typeparam name="T">The type to serialize to or from.</typeparam>
    public class FileHandler<T> : IFileHandler<T>
    {
        private readonly IOptions<Datafile> _datafile;
        private readonly IFileProvider _fileProvider;
        private readonly IJsonSerialization _serialization;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileHandler{T}"/> class.
        /// </summary>
        public FileHandler(IFileProvider fileProvider, IOptions<Datafile> datafile, IJsonSerialization serialization)
        {
            _fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
            _datafile = datafile ?? throw new ArgumentNullException(nameof(datafile));
            _serialization = serialization ?? throw new ArgumentNullException(nameof(serialization));
        }

        /// <summary>
        /// Get the file as an object of type T.
        /// </summary>
        public T Get()
        {
            var fileInfo = _fileProvider.GetFileInfo(_datafile.Value.FileName);
            return _serialization.Deserialize<T>(fileInfo);
        }

        /// <summary>
        /// Get all objects of this type.
        /// </summary>
        public IList<T> GetList()
        {
            var fileInfo = _fileProvider.GetFileInfo(_datafile.Value.FileName);
            return _serialization.Deserialize<IList<T>>(fileInfo);
        }
    }
}