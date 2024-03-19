namespace AEncoding
{
    using System.Numerics;

    public static class EncodeMessage
    {
        public static string Encode(string message, string chars)
        {
            message = EncodeMessage.ConvertToInt(message);

            var encodedMessage = string.Empty;
            var originalMessage = message.Split(" ");

            List<BigInteger> intMessage = new List<BigInteger>();
            foreach (var s in originalMessage) intMessage.Add(BigInteger.Parse(s));

            foreach (var i in intMessage)
            {
                var messageInt = i;
                while (messageInt != 0)
                {
                    encodedMessage += chars[(int)(messageInt % 35)];
                    messageInt /= 36;
                }

                encodedMessage += " ";
            }

            return encodedMessage.Trim();
        }
        private static string ConvertToInt(string message)
        {
            message.ToCharArray();

            var intList = string.Empty;
            var intWord = string.Empty;
            var intCharLength = string.Empty;

            foreach (var character in message)
                if (character.ToString().Equals(" "))
                {
                    intList += intWord + " " + intCharLength + " ";
                    intWord = string.Empty;
                    intCharLength = string.Empty;
                }
                else
                {
                    var intChar = Convert.ToInt32(character).ToString();
                    intWord += intChar;
                    intCharLength += intChar.Length.ToString();
                }

            intList += intWord + " " + intCharLength;

            return intList.Trim();
        }
    }
}
