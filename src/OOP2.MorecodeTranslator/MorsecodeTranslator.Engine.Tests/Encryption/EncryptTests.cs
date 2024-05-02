namespace MorsecodeTranslator.Engine.Encryption.Tests;

using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class EncryptTests
{
    [TestMethod]
    public void EncryptMessage_NotNull_Test()
    {
        //ARRANGE
        var originalMessage = "Hello, World!";
        var key = "testKey";
        var messageBytes = Encoding.UTF8.GetBytes(originalMessage);

        //ACT
        var encryptedMessage = Encrypt.EncryptMessage(messageBytes, key);

        //ASSERT
        Assert.IsNotNull(encryptedMessage);
    }

    [TestMethod]
    public void EncryptMessage_NullMessage_Test()
    {
        //ARRANGE
        string originalMessage = null;
        var key = "testKey";
        byte[] messageBytes = null;

        //ACT
        //ASSERT
        Assert.ThrowsException<ArgumentNullException>(() => Encrypt.EncryptMessage(messageBytes, key));
    }

    [TestMethod]
    public void EncryptMessage_NullKey_Test()
    {
        //ARRANGE
        var originalMessage = "Hello, World!";
        string key = null;
        var messageBytes = Encoding.UTF8.GetBytes(originalMessage);

        //ACT
        //ASSERT
        Assert.ThrowsException<ArgumentNullException>(() => Encrypt.EncryptMessage(messageBytes, key));
    }
}