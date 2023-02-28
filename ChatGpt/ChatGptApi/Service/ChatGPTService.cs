using Newtonsoft.Json;
using OpenAI_API.Completions;
using OpenAI_API;
using ChatGptApi.Model;
using System.Net.Http.Headers;
using System.Text;
using System.Drawing;

namespace ChatGptApi.Service
{
    public class ChatGPTService
    {
        private readonly string ApiKey;
        private readonly IHttpClientFactory _clientFactory;

        public ChatGPTService(string _apiKey, IHttpClientFactory clientFactory)
        {
            ApiKey = _apiKey;
            _clientFactory = clientFactory;
        }

        public async Task<string> AnswerAndQuestion(string question, int token)
        {

            OpenAIAPI aIAPI = new OpenAIAPI(ApiKey);
            string response = string.Empty;
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = question;
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = token;

            var result = await aIAPI.Completions.CreateCompletionAsync(completion);

            if (result != null)
            {
                result.Completions.ForEach(x =>
                {
                    response += x.Text;
                });
            }

            return response;
        }

        public async Task<string> Code(string code, int token)
        {
            OpenAIAPI aIAPI = new OpenAIAPI(ApiKey);
            string response = string.Empty;
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = code;
            completion.Model = OpenAI_API.Models.Model.DavinciCode;
            completion.MaxTokens = token;

            var result = await aIAPI.Completions.CreateCompletionAsync(completion);

            if (result != null)
            {
                result.Completions.ForEach(x =>
                {
                    response += x.Text;
                });

            }

            return response;

        }

        public async Task<string> HttpClientAnswerAndQuestion(string question, int token)
        {
            var client = _clientFactory.CreateClient("ChatGptClientCompletions");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

            var requestBody = JsonConvert.SerializeObject(new InputChatGpt(question, token));

            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("", content);

            var result = await response.Content.ReadFromJsonAsync<ResponseChatGpt>();

            return result.choices.FirstOrDefault().text;
        }

        public async Task<string> GenerateImagem(string request, string size) {

            var client = _clientFactory.CreateClient("ChatGptClientImage");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

            var requestBody = JsonConvert.SerializeObject(new ImageInputChatGpt(request, size));

            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("", content);

            var result = await response.Content.ReadFromJsonAsync<ImageResponseChatGpt>();

            string responseString = string.Empty;

            result.data.ForEach(x =>
            {
                responseString += $" \n {x.url}";
            });

           return responseString;
        }
    }

}
