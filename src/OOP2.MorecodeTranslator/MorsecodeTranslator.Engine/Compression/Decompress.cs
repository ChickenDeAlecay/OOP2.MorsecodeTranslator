namespace MorsecodeTranslator.Engine.Compression;

using System.IO.Compression;

public static class Decompress
{
    public static byte[] DecompressBytes(byte[] data)
    {
        using var compressedStream = new MemoryStream(data);
        using var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress);
        using var resultStream = new MemoryStream();
        zipStream.CopyTo(resultStream);
        return resultStream.ToArray();
    }
}