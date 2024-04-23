namespace AEncoding;

using System.Numerics;

public static class DecodeMessage
{
    public static byte[] Decode(string message, string baseChars)
    {
        var charArray = baseChars.ToCharArray();
        var decodedValue = BigInteger.Zero;

        foreach (var charIndex in message.Select(encodedChar => Array.IndexOf(charArray, encodedChar)))
        {
            if (charIndex is -1)
                throw new ArgumentException("The encoding contains unknown characters", nameof(message));

            decodedValue = BigInteger.Add(BigInteger.Multiply(decodedValue, baseChars.Length), charIndex);
        }

        return decodedValue.ToByteArray();
    }
}