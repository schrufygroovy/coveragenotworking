using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Daxi.Libraries.MemoryStreamer
{
    public class ZipStreamer
    {
        // see: https://forums.asp.net/t/2144882.aspx?Dynamically+creating+Zip+file+in+Web+API
        public MemoryStream GetZipStream(Dictionary<string, Stream> streams)
        {
            byte[] buffer = new byte[6500];
            var zipMemoryStream = new MemoryStream();
            using (var zipArchive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var kv in streams)
                {
                    string fileName = kv.Key;
                    using (var streamInput = kv.Value)
                    {
                        var entry = zipArchive.CreateEntry(fileName, CompressionLevel.Fastest);
                        using (var entryStream = entry.Open())
                        {
                            var readCount = streamInput.Read(buffer, 0, buffer.Length);
                            while (readCount > 0)
                            {
                                entryStream.Write(buffer, 0, readCount);
                                readCount = streamInput.Read(buffer, 0, buffer.Length);
                            }
                            entryStream.Flush();
                        }
                    }
                }
            }

            zipMemoryStream.Position = 0;
            return zipMemoryStream;
        }
    }
}
