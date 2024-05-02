namespace MorsecodeTranslator.Engine.Translation.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MorsecodeTranslator.Engine.Compression;
using MorsecodeTranslator.Engine.Encryption;

[TestClass]
public class TranslateToMorsecodeTests
{
    private readonly string[,] translationTable = { { "a", ".-" }, { "b", "-..." } };
    private TranslateToMorsecode translateToMorsecode = null!;

    [TestInitialize]
    public void Setup()
    {
        var translationSet = new ReadTranslationSet(this.translationTable);
        this.translateToMorsecode = new TranslateToMorsecode(translationSet, new Gzip(), new AesEncryption());
    }

    [TestMethod]
    public void ProcessData_NotNull_Test()
    {
        //ARRANGE
        var originalMessage = "AB";
        var key = "testKey";

        //ACT
        var processedMessage = this.translateToMorsecode.ProcessData(originalMessage, key);

        //ASSERT
        Assert.IsNotNull(processedMessage);
    }

    [TestMethod]
    public void ProcessData_NullMessage_Test()
    {
        //ARRANGE
        string originalMessage = null;
        var key = "testKey";

        //ACT
        //ASSERT
        Assert.ThrowsException<ArgumentNullException>(() =>
            this.translateToMorsecode.ProcessData(originalMessage, key));
    }

    [TestMethod]
    public void ProcessData_NullKey_Test()
    {
        //ARRANGE
        var originalMessage = "AB";
        string key = null;

        //ACT
        //ASSERT
        Assert.ThrowsException<ArgumentNullException>(() =>
            this.translateToMorsecode.ProcessData(originalMessage, key));
    }
}