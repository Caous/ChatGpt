namespace ChatGptApi.Model
{
    public class ImageInputChatGpt
    {
        public ImageInputChatGpt(string image, string siz)
        {
            this.n = 2;
            this.prompt = image;
            this.size = siz;
        }
        public string prompt { get; set; }
        public int n { get; set; }
        public string size { get; set; }
    }
}
