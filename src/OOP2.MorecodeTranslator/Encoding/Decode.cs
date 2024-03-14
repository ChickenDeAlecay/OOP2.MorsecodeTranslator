namespace Encoding;

public class Decode
{
    internal static class DecodeMessage
    {
        public static string Decode(string message, string chars)
        {
            message = BackToString(message);

            long tempMessage = 0;
            var decodedMessage = string.Empty;

            var origionalMessage = message.Split(" ");

            var listMessage = new List<string>();
            foreach (var s in origionalMessage) listMessage.Add(s);

            foreach (var word in listMessage)
            {
                for (var i = 0; i < word.Length; i++)
                    tempMessage += chars.IndexOf(word[i]) * (long)Math.Pow(36, i);

                decodedMessage += tempMessage + " ";
                tempMessage = 0;
            }

            return decodedMessage.Trim();
        }

        private static string BackToString(string message)
        {
            var unEncodedMessage = string.Empty;
            var origionalMessage = message.Split(" ");

            var intMessage = new List<long>();
            foreach (var s in origionalMessage) intMessage.Add(long.Parse(s));

            for (var i = 0; i < intMessage.Count; i += 2)
            {
                var word = intMessage[i].ToString();
                var code = intMessage[i + 1].ToString().ToCharArray();
                foreach (var length in code)
                {
                    int temp;
                    var numLength = int.Parse(length.ToString());

                    temp = int.Parse(word.Substring(0, numLength));
                    unEncodedMessage += (char)temp;
                    word = word.Remove(0, numLength);
                }

                unEncodedMessage += " ";
            }

            return unEncodedMessage.Trim();
        }
    }
}