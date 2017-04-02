using Microsoft.Extensions.FileProviders;

namespace Utilities.Serialization
{
    public interface IJsonSerialization
    {
        T Deserialize<T>(IFileInfo fileInfo);
    }
}