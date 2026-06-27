namespace WpfApp2
{
    public class Chatbot
    {
        public string GetResponse(string input)
        {
            input = input.ToLower();

            if (input.Contains("cyber"))
                return "Cybersecurity protects systems and data from attacks.";

            if (input.Contains("password"))
                return "Use strong, unique passwords with symbols.";

            if (input.Contains("phishing"))
                return "Phishing is a scam to steal sensitive data.";

            return "Interesting... tell me more!";
        }

        public string DetectEmotion(string input)
        {
            input = input.ToLower();

            if (input.Contains("sad"))
                return "I'm here for you 💙";

            if (input.Contains("happy"))
                return "That's great 😄";

            return null;
        }
    }
}