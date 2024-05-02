namespace MorsecodeTranslator.Engine.Compression;

public interface IGzip
{
    byte[] CompressBytes(byte[] bytes);
    byte[] DecompressBytes(byte[] bytes);
}