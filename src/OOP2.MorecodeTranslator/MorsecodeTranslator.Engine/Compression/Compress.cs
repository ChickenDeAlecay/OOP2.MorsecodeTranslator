namespace MorsecodeTranslator.Engine.Compression;

using System.IO.Compression;

public static class Compress
{
    public static byte[] CompressBytes(byte[] buffer)
    {
        using var memStream = new MemoryStream();

        using (var gZipStream = new GZipStream(memStream, CompressionMode.Compress, true))
        {
            gZipStream.Write(buffer, 0, buffer.Length);
        }

        return memStream.ToArray();
    }
}