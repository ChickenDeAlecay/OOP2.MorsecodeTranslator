namespace AEncoding;

using System.Numerics;
using System.Text;

public static class EncodeMessage
{
    public static string Encode(byte[] message, string chars)
    {
        var dividend = new BigInteger(message);
        var builder = new StringBuilder();
        while (dividend != 0)
        {
            dividend = BigInteger.DivRem(dividend, chars.Length, out var remainder);
            builder.Insert(0, chars[Math.Abs((int)remainder)]);
        }

        return builder.ToString();
    }
}