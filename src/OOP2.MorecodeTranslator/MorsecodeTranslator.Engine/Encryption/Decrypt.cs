namespace MorsecodeTranslator.Engine.Encryption;

using System.Security.Cryptography;

public static class Decrypt
{
    //https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
    public static byte[] DecryptMessage(byte[] cipherText, string key)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException(nameof(cipherText));
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException(nameof(key));

        // Convert the cipher text to a byte array
        var fullCipher = cipherText;

        // Extract the salt and IV from the start of the cipher text
        var salt = new byte[16];
        var iv = new byte[16];
        Buffer.BlockCopy(fullCipher, 0, salt, 0, salt.Length);
        Buffer.BlockCopy(fullCipher, salt.Length, iv, 0, iv.Length);

        var passwordBytes = new Rfc2898DeriveBytes(key, salt, 10000, HashAlgorithmName.SHA256);

        // Create an Aes object with the specified key and IV.
        using var aesAlg = Aes.Create();
        aesAlg.Key = passwordBytes.GetBytes(aesAlg.KeySize / 8);
        aesAlg.IV = iv;
        aesAlg.Padding = PaddingMode.PKCS7;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for decryption.
        using var msDecrypt = new MemoryStream(fullCipher, salt.Length + iv.Length,
            fullCipher.Length - salt.Length - iv.Length);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var memoryStream = new MemoryStream();
        csDecrypt.CopyTo(memoryStream);

        return memoryStream.ToArray();
    }
}