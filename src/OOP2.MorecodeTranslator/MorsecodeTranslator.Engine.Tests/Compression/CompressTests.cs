namespace MorsecodeTranslator.Engine.Compression.Tests;

using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CompressTests
{
    [TestMethod]
    public void CompressBytes_NotNull_Test()
    {
        //ARRANGE
        var originalMessage = "Hello, World!";
        var messageBytes = Encoding.UTF8.GetBytes(originalMessage);

        //ACT
        var compressedBytes = Compress.CompressBytes(messageBytes);

        //ASSERT
        Assert.IsNotNull(compressedBytes);
        var decompressedBytes = Decompress.DecompressBytes(compressedBytes);
        var decompressedMessage = Encoding.UTF8.GetString(decompressedBytes);
        Assert.AreEqual(originalMessage, decompressedMessage);
    }

    [TestMethod]
    public void CompressBytes_NullBytes_Test()
    {
        //ARRANGE
        byte[] messageBytes = null;

        //ACT
        //ASSERT
        Assert.ThrowsException<NullReferenceException>(() => Compress.CompressBytes(messageBytes));
    }
}