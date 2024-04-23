namespace Encryption;

using System.Security.Cryptography;
using System.Text;

public static class Encrypt
{
    public static byte[] EncryptMessage(string message, string key, byte[] IV)
    {
        // Check arguments.
        if (message == null || message.Length <= 0)
            throw new ArgumentNullException("message");
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException("key");

        // Derive a new password using the PBKDF2 algorithm and a random salt
        var passwordBytes = new Rfc2898DeriveBytes(key, 20, 10, HashAlgorithmName.SHA256);

        // Create an Aes object
        // with the specified key and IV.
        using var aesAlg = Aes.Create();
        aesAlg.Key = passwordBytes.GetBytes(32);
        aesAlg.IV = IV;
        aesAlg.Padding = PaddingMode.PKCS7;

        // Create an encryptor to perform the stream transform.
        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using var msEncrypt = new MemoryStream();
        using (var cswEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            var plainBytes = Encoding.UTF8.GetBytes(message);
            cswEncrypt.Write(plainBytes, 0, plainBytes.Length);
        }

        //using (var swEncrypt = new StreamWriter(cswEncrypt))
        //{
        //    //Write all data to the stream.
        //    swEncrypt.Write(message);
        //}

        var encrypted = msEncrypt.ToArray();

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }
}