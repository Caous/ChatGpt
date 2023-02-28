namespace ChatGptApi.Model
{
    public class Datum
    {
        public string url { get; set; }
    }

    public class ImageResponseChatGpt
    {
        public int created { get; set; }
        public List<Datum> data { get; set; }
    }
}
