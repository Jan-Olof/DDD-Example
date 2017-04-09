using Newtonsoft.Json;
using System.IO;

namespace InfrastructureLayer.Files
{
    public class JsonSerialization : IJsonSerialization
    {
        public T Deserialize<T>(Stream stream)
        {
            T data;

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                var serializer = new JsonSerializer();
                data = serializer.Deserialize<T>(jsonTextReader);
            }

            return data;
        }

        public void Serialize(object value, Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                JsonSerializer ser = new JsonSerializer();
                ser.Serialize(jsonWriter, value);
                jsonWriter.Flush();
            }
        }
    }
}