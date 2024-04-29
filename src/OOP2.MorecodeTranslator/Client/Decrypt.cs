namespace Client;

using System.Security.Cryptography;

public static class Decrypt
{
    //https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
    public static string DecryptMessage(byte[] cipherText, string key)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException("Key");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;
        var passwordBytes = new Rfc2898DeriveBytes(key, BitConverter.GetBytes(20), 10000, HashAlgorithmName.SHA256);

        // Create an Aes object
        // with the specified key and IV.
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = passwordBytes.GetBytes(32);

            // Create the streams used for decryption.
            using (var msDecrypt = new MemoryStream(cipherText))
            {
                // Read the IV from the start of the stream
                var iv = new byte[16];
                msDecrypt.Read(iv, 0, iv.Length);
                aesAlg.IV = iv;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}