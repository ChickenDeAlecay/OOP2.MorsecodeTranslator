namespace MorsecodeTranslator.Core.Encryption;

using System.Security.Cryptography;

public static class Encrypt
{
    //https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
    public static byte[] EncryptMessage(string message, string key)
    {
        // Check arguments.
        if (message == null || message.Length <= 0)
            throw new ArgumentNullException(nameof(message));
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException(nameof(key));

        // Derive a new password using the PBKDF2 algorithm and a random salt
        var passwordBytes = new Rfc2898DeriveBytes(key, BitConverter.GetBytes(20), 10000, HashAlgorithmName.SHA256);

        // Create an Aes object
        // with the specified key and IV.
        using var aesAlg = Aes.Create();
        aesAlg.Key = passwordBytes.GetBytes(32);
        // Create an encryptor to perform the stream transform.
        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using var msEncrypt = new MemoryStream();
        // Write the IV to the start of the stream
        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            //Write all data to the stream.
            swEncrypt.Write(message);
        }

        var encrypted = msEncrypt.ToArray();


        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }
}