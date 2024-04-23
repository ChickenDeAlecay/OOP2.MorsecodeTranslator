namespace AEncoding;

using System.Numerics;

public static class DecodeMessage
{
    public static byte[] Decode(string message, string baseChars)
    {
        var charArray = baseChars.ToCharArray();
        var decodedValue = BigInteger.Zero;


        foreach (var letter in message)
        {
            var charIndex = Array.IndexOf(charArray, letter);

            decodedValue = BigInteger.Add(BigInteger.Multiply(decodedValue, baseChars.Length), charIndex);
        }

        return decodedValue.ToByteArray();
    }
}