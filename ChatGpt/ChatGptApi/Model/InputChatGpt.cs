using ChatGptApi.Training;

namespace ChatGptApi.Model
{
    public class InputChatGpt
    {
        public InputChatGpt(string request, int token)
        {
            this.prompt = new TrainingChatGpt().TrainingBot(request);
            this.max_tokens = token;
            this.temperature = 0.2m;
            this.model = "text-davinci-003";
        }

        public string model { get; set; }
        public string prompt { get; set; }
        public int max_tokens { get; set; }
        public decimal temperature { get; set; }
    }

}
