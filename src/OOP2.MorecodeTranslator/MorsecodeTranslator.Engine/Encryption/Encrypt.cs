namespace MorsecodeTranslator.Engine.Encryption;

using System.Security.Cryptography;

public static class Encrypt
{
    //https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
    public static byte[] EncryptMessage(byte[] message, string key)
    {
        // Check arguments.
        if (message == null || message.Length <= 0)
            throw new ArgumentNullException(nameof(message));
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException(nameof(key));

        // Generate a salt
        var salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        var passwordBytes = new Rfc2898DeriveBytes(key, salt, 10000, HashAlgorithmName.SHA256);

        //generate an IV
        var ivBytes = new byte[16];
        RandomNumberGenerator.Create().GetBytes(ivBytes);

        // Create an Aes object with the specified key and IV.
        using var aesAlg = Aes.Create();
        aesAlg.Key = passwordBytes.GetBytes(aesAlg.KeySize / 8);
        aesAlg.IV = ivBytes;
        aesAlg.Padding = PaddingMode.PKCS7;

        // Create an encryptor to perform the stream transform.
        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using var msEncrypt = new MemoryStream();

        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            // Write all the data to be encrypted to the stream.
            csEncrypt.Write(message, 0, message.Length);
        }

        // Extract the encrypted bytes from the MemoryStream.
        var encryptedData = msEncrypt.ToArray();

        // Prepare the final byte array which includes salt, IV, and encrypted data.
        var encrypted = new byte[salt.Length + aesAlg.IV.Length + encryptedData.Length];

        // Copy salt, IV, and encrypted data into the result byte array.
        Buffer.BlockCopy(salt, 0, encrypted, 0, salt.Length);
        Buffer.BlockCopy(aesAlg.IV, 0, encrypted, salt.Length, aesAlg.IV.Length);
        Buffer.BlockCopy(encryptedData, 0, encrypted, salt.Length + aesAlg.IV.Length, encryptedData.Length);

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }
}