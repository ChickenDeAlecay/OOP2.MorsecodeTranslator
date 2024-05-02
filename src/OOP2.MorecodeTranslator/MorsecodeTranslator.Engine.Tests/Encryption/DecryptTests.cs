namespace MorsecodeTranslator.Engine.Encryption.Tests;

using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class DecryptTests
{
    [TestMethod]
    public void DecryptMessage_NotNull_Test()
    {
        //ARRANGE
        var originalMessage = "Hello, World!";
        var key = "testKey";
        var messageBytes = Encoding.UTF8.GetBytes(originalMessage);
        var encryptedMessage = Encrypt.EncryptMessage(messageBytes, key);

        //ACT
        var decryptedMessage = Decrypt.DecryptMessage(encryptedMessage, key);

        //ASSERT
        Assert.IsNotNull(decryptedMessage);
    }

    [TestMethod]
    public void DecryptMessage_NullMessage_Test()
    {
        //ARRANGE
        byte[] encryptedMessage = null;
        var key = "testKey";

        //ACT
        //ASSERT
        Assert.ThrowsException<ArgumentNullException>(() => Decrypt.DecryptMessage(encryptedMessage, key));
    }

    [TestMethod]
    public void DecryptMessage_NullKey_Test()
    {
        //ARRANGE
        var originalMessage = "Hello, World!";
        var key = "testKey";
        var messageBytes = Encoding.UTF8.GetBytes(originalMessage);
        var encryptedMessage = Encrypt.EncryptMessage(messageBytes, key);
        string nullKey = null;

        //ACT
        //ASSERT
        Assert.ThrowsException<ArgumentNullException>(() => Decrypt.DecryptMessage(encryptedMessage, nullKey));
    }
}