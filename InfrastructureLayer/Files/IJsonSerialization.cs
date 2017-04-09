using System.IO;

namespace InfrastructureLayer.Files
{
    public interface IJsonSerialization
    {
        T Deserialize<T>(Stream s);
    }
}