namespace Encryption;

using System.Security.Cryptography;

public static class Encrypt
{
    public static string EncryptMessage(string message, string key)
    {
        // Check arguments.
        if (message == null || message.Length <= 0)
            throw new ArgumentNullException("message");
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException("key");
        string encrypted;

        // Derive a new password using the PBKDF2 algorithm and a random salt
        var passwordBytes = new Rfc2898DeriveBytes(key, 20);

        // Create an Aes object
        // with the specified key and IV.
        using var aesAlg = Aes.Create();
        aesAlg.Key = passwordBytes.GetBytes(32);

        // Create an encryptor to perform the stream transform.
        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using var msEncrypt = new MemoryStream();
        using var cswEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(cswEncrypt))
        {
            //Write all data to the stream.
            swEncrypt.Write(message);
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted = "";
    }
}