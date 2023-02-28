using System.Text;

namespace ChatGptApi.Training
{
    public class TrainingChatGpt
    {

        public TrainingChatGpt()
        {

        }
        public string TrainingBot(string question)
        {
            string sentence = "ChatGpt olá tudo bem? Por favor toda resposta que você for me dar usar a seguinte sentença: Ultron encontrou isto.";
            StringBuilder st = new StringBuilder(sentence);
            st.AppendLine(question);
            return st.ToString();

        }

    }
}
