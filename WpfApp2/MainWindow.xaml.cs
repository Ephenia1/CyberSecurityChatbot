using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private Chatbot bot = new Chatbot();
        private Quiz quiz = new Quiz();
        private bool isQuizMode = false;

        public MainWindow()
        {
            InitializeComponent();
            AddMessage("Bot", "Welcome! Type 'quiz' to test your cybersecurity knowledge.");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private async void SendMessage()
        {
            string input = UserInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AddMessage("You", input);
            UserInput.Clear();

            await TypingEffect();

            if (input.ToLower() == "quiz")
            {
                isQuizMode = true;
                quiz.Reset();
                AddMessage("Bot", "Starting quiz...");
                AddMessage("Bot", quiz.GetNextQuestion());
                return;
            }

            if (isQuizMode)
            {
                bool correct = quiz.CheckAnswer(input);
                AddMessage("Bot", correct ? "Correct ✅" : "Wrong ❌");

                string next = quiz.GetNextQuestion();

                if (next == null)
                {
                    AddMessage("Bot", $"Finished! Score: {quiz.Score}/{quiz.Total}");
                    isQuizMode = false;
                }
                else
                {
                    AddMessage("Bot", next);
                }
                return;
            }

            string emotion = bot.DetectEmotion(input);

            if (!string.IsNullOrEmpty(emotion))
                AddMessage("Bot", emotion);
            else
                AddMessage("Bot", bot.GetResponse(input));
        }

        private void AddMessage(string sender, string message)
        {
            Border bubble = new Border
            {
                Background = sender == "You" ? new SolidColorBrush(Color.FromRgb(0, 122, 204))
                                             : new SolidColorBrush(Color.FromRgb(45, 45, 48)),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(10),
                Margin = new Thickness(5),
                HorizontalAlignment = sender == "You" ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                MaxWidth = 500
            };

            TextBlock text = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = text;
            ChatPanel.Children.Add(bubble);
            ChatScroll.ScrollToEnd();
        }

        private async Task TypingEffect()
        {
            AddMessage("Bot", "Typing...");
            await Task.Delay(500);

            // Remove last message (Typing...)
            if (ChatPanel.Children.Count > 0)
                ChatPanel.Children.RemoveAt(ChatPanel.Children.Count - 1);
        }
    }
}