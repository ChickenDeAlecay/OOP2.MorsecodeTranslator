namespace MorsecodeTranslator.Engine.Compression.Tests;

using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class DecompressTests
{
    [TestMethod]
    public void DecompressBytes_NotNull_Test()
    {
        //ARRANGE
        var originalMessage = "Hello, World!";
        var messageBytes = Encoding.UTF8.GetBytes(originalMessage);
        var compressedBytes = Compress.CompressBytes(messageBytes);

        //ACT
        var decompressedBytes = Decompress.DecompressBytes(compressedBytes);

        //ASSERT
        Assert.IsNotNull(decompressedBytes);
        var decompressedMessage = Encoding.UTF8.GetString(decompressedBytes);
        Assert.AreEqual(originalMessage, decompressedMessage);
    }

    [TestMethod]
    public void DecompressBytes_NullBytes_Test()
    {
        //ARRANGE
        byte[] compressedBytes = null;

        //ACT
        //ASSERT
        Assert.ThrowsException<ArgumentNullException>(() => Decompress.DecompressBytes(compressedBytes));
    }
}