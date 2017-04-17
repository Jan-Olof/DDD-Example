using System.IO;

namespace InfrastructureLayer.Files
{
    /// <summary>
    /// The JsonSerialization interface.
    /// </summary>
    public interface IJsonSerialization
    {
        /// <summary>
        /// Deserialize from JSON.
        /// </summary>
        T Deserialize<T>(Stream s);

        /// <summary>
        /// Serialize to JSON.
        /// </summary>
        void Serialize<T>(T value, Stream stream);
    }
}