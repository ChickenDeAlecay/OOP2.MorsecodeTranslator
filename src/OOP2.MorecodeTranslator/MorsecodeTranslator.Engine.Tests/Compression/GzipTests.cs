namespace MorsecodeTranslator.Engine.Tests.Compression;

using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MorsecodeTranslator.Engine.Compression;

[TestClass]
public class GzipTests
{
    private IGzip gzip = null!;

    [TestInitialize]
    public void Setup()
    {
        this.gzip = new Gzip();
    }

    [TestMethod]
    public void CompressBytes_NotNull_Test()
    {
        //ARRANGE
        var originalMessage = "Hello, World!";
        var messageBytes = Encoding.UTF8.GetBytes(originalMessage);

        //ACT
        var compressedBytes = this.gzip.CompressBytes(messageBytes);

        //ASSERT
        Assert.IsNotNull(compressedBytes);
        var decompressedBytes = this.gzip.DecompressBytes(compressedBytes);
        var decompressedMessage = Encoding.UTF8.GetString(decompressedBytes);
        Assert.AreEqual(originalMessage, decompressedMessage);
    }

    [TestMethod]
    public void CompressBytes_NullBytes_Test()
    {
        //ARRANGE
        byte[] messageBytes = null!;

        //ACT
        //ASSERT
        Assert.ThrowsException<NullReferenceException>(() => this.gzip.CompressBytes(messageBytes));
    }

    [TestMethod]
    public void DecompressBytes_NotNull_Test()
    {
        //ARRANGE
        var originalMessage = "Hello, World!";
        var messageBytes = Encoding.UTF8.GetBytes(originalMessage);
        var compressedBytes = this.gzip.CompressBytes(messageBytes);

        //ACT
        var decompressedBytes = this.gzip.DecompressBytes(compressedBytes);

        //ASSERT
        Assert.IsNotNull(decompressedBytes);
        var decompressedMessage = Encoding.UTF8.GetString(decompressedBytes);
        Assert.AreEqual(originalMessage, decompressedMessage);
    }

    [TestMethod]
    public void DecompressBytes_NullBytes_Test()
    {
        //ARRANGE
        byte[] compressedBytes = null!;

        //ACT
        //ASSERT
        Assert.ThrowsException<ArgumentNullException>(() => this.gzip.DecompressBytes(compressedBytes));
    }
}