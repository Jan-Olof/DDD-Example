using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System.IO;

namespace Utilities.Serialization
{
    public class JsonSerialization : IJsonSerialization
    {
        // TODO: Fix this and use in FileHandler
        //using System.IO.FileSystem;
        //FileStream fileStream = new FileStream("file.txt", FileMode.Open);

        public static void Serialize(object value, Stream s)
        {
            using (StreamWriter writer = new StreamWriter(s))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                JsonSerializer ser = new JsonSerializer();
                ser.Serialize(jsonWriter, value);
                jsonWriter.Flush();
            }
        }

        public T Deserialize<T>(IFileInfo fileInfo)
        {
            T data;

            using (var stream = fileInfo.CreateReadStream())
            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                var serializer = new JsonSerializer();
                data = serializer.Deserialize<T>(jsonTextReader);
            }

            return data;
        }
    }
}