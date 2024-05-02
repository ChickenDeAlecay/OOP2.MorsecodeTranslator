namespace MorsecodeTranslator.Engine.Compression;

using System.IO.Compression;

public static class Compress
{
    public static byte[] CompressBytes(byte[] data)
    {
        using var compressedStream = new MemoryStream();
        using var zipStream = new GZipStream(compressedStream, CompressionMode.Compress);
        zipStream.Write(data, 0, data.Length);
        zipStream.Close();
        return compressedStream.ToArray();
    }
}