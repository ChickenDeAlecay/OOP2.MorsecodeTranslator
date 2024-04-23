namespace AEncoding;

using System;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

public static class DecodeMessage
{
    public static byte[] Decode(string message, string baseChars)
    {
        //var charArray = baseChars.ToCharArray();
        //var decodedValue = BigInteger.Zero;


        //foreach (var letter in message)
        //{
        //    var charIndex = Array.IndexOf(charArray, letter);

        //    decodedValue = BigInteger.Add(BigInteger.Multiply(decodedValue, baseChars.Length), charIndex);
        //}

        //return decodedValue.ToByteArray();

        BigInteger value = 0;

        for (var i = 0; i < message.Length; i++) value = value * baseChars.Length + baseChars.IndexOf(message[i]);

        return value.ToByteArray().ToArray();
    }
}