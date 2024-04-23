namespace Encryption;

using System.Security.Cryptography;
using System.Text;

public static class Decrypt
{
    public static string DecryptMessage(byte[] cipherText, string key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException("Key");

        // Declare the string used to hold
        // the decrypted text.

        // Derive a new password using the PBKDF2 algorithm and a random salt
        var passwordBytes = new Rfc2898DeriveBytes(key, 20, 10, HashAlgorithmName.SHA256);


        // Create an Aes object
        // with the specified key and IV.
        using var aesAlg = Aes.Create();
        aesAlg.Key = passwordBytes.GetBytes(32);
        aesAlg.IV = IV;

        // Create a decryptor to perform the stream transform.
        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        byte[] decryptedBytes;

        using (var msDecrypt = new MemoryStream(cipherText))
        {
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (var msPlain = new MemoryStream())
                {
                    csDecrypt.CopyTo(msPlain);
                    decryptedBytes = msPlain.ToArray();
                }
            }
        }

        return Encoding.UTF8.GetString(decryptedBytes);


        //// Create the streams used for decryption.
        //using var msDecrypt = new MemoryStream(cipherText);
        //using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        //using var srDecrypt = new StreamReader(csDecrypt);
        //// Read the decrypted bytes from the decrypting stream
        //// and place them in a string.
        //var plaintext = srDecrypt.ReadToEnd();

        //return plaintext;
    }
}