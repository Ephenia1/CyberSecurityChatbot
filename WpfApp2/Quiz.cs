using System.Collections.Generic;

namespace WpfApp2
{
    public class Quiz
    {
        private int index = 0;
        public int Score { get; private set; } = 0;
        public int Total => questions.Count;

        private List<(string Q, string A)> questions = new List<(string, string)>
        {
            ("What is phishing?", "scam"),
            ("VPN stands for?", "virtual private network"),
            ("Is 123456 safe? (yes/no)", "no")
        };

        public void Reset()
        {
            index = 0;
            Score = 0;
        }

        public string GetNextQuestion()
        {
            if (index >= questions.Count) return null;
            return questions[index].Q;
        }

        public bool CheckAnswer(string input)
        {
            bool correct = input.ToLower().Contains(questions[index].A);
            if (correct) Score++;
            index++;
            return correct;
        }
    }
}