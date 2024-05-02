namespace MorsecodeTranslator.Engine.Encryption;

public interface IAesEncryption
{
    public byte[] EncryptMessage(byte[] message, string key);

    public byte[] DecryptMessage(byte[] cipherText, string key);
}