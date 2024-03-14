using System.Security.Cryptography;
using System.Text;

namespace Encryption;

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

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.ASCII.GetBytes(key);

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(message);
                    }
                    encrypted = msEncrypt.ToString();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }
}