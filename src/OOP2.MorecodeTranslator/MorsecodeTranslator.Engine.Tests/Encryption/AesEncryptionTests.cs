namespace MorsecodeTranslator.Engine.Tests.Encryption;

using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MorsecodeTranslator.Engine.Encryption;

[TestClass]
public class AesEncryptionTests
{
    private IAesEncryption aesEncryption = null!;

    [TestInitialize]
    public void Setup()
    {
        this.aesEncryption = new AesEncryption();
    }

    [TestMethod]
    public void EncryptMessage_NotNull_Test()
    {
        //ARRANGE
        var originalMessage = "Hello, World!";
        var key = "testKey";
        var messageBytes = Encoding.UTF8.GetBytes(originalMessage);

        //ACT
        var encryptedMessage = this.aesEncryption.EncryptMessage(messageBytes, key);

        //ASSERT
        Assert.IsNotNull(encryptedMessage);
    }

    [TestMethod]
    public void EncryptMessage_NullMessage_Test()
    {
        //ARRANGE
        var key = "testKey";
        byte[] messageBytes = null!;

        //ACT
        //ASSERT
        Assert.ThrowsException<ArgumentNullException>(() => this.aesEncryption.EncryptMessage(messageBytes, key));
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
        Assert.ThrowsException<ArgumentNullException>(() => this.aesEncryption.EncryptMessage(messageBytes, key));
    }

    [TestMethod]
    public void DecryptMessage_NotNull_Test()
    {
        //ARRANGE
        var originalMessage = "Hello, World!";
        var key = "testKey";
        var messageBytes = Encoding.UTF8.GetBytes(originalMessage);
        var encryptedMessage = this.aesEncryption.EncryptMessage(messageBytes, key);

        //ACT
        var decryptedMessage = this.aesEncryption.DecryptMessage(encryptedMessage, key);

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
        Assert.ThrowsException<ArgumentNullException>(() => this.aesEncryption.DecryptMessage(encryptedMessage, key));
    }

    [TestMethod]
    public void DecryptMessage_NullKey_Test()
    {
        //ARRANGE
        var originalMessage = "Hello, World!";
        var key = "testKey";
        var messageBytes = Encoding.UTF8.GetBytes(originalMessage);
        var encryptedMessage = this.aesEncryption.EncryptMessage(messageBytes, key);
        string nullKey = null;

        //ACT
        //ASSERT
        Assert.ThrowsException<ArgumentNullException>(
            () => this.aesEncryption.DecryptMessage(encryptedMessage, nullKey));
    }
}