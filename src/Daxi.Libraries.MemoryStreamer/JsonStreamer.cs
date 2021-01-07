using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Daxi.Libraries.MemoryStreamer
{
    public class JsonStreamer
    {
        public MemoryStream GetMemoryStream<T>(
            T objectToStream,
            bool formattingIndented)
        {
            var memoryStream = new MemoryStream();
            using (var textWriter = new StreamWriter(memoryStream, Encoding.UTF8, 1024 * 100, leaveOpen: true))
            {
                var serializer = new JsonSerializer();
                serializer.Formatting = formattingIndented ? Formatting.Indented : Formatting.None;
                serializer.Serialize(textWriter, objectToStream);
            }
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
