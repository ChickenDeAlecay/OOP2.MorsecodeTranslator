namespace MorsecodeTranslator.Engine.Compression;

using System.IO.Compression;

public static class Decompress
{
    public static byte[] DecompressBytes(byte[] compressedData)
    {
        using var memStream = new MemoryStream(compressedData);

        using var gZipStream = new GZipStream(memStream, CompressionMode.Decompress);

        using var resultStream = new MemoryStream();

        gZipStream.CopyTo(resultStream);

        return resultStream.ToArray();
    }
}