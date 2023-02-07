using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using OpenAI_API;
using OpenAI_API.Completions;

namespace ChatGptApi.Service
{
    public class ChatGPTService
    {
        private readonly string ApiKey;
        public ChatGPTService(string _apiKey)
        {
            ApiKey = _apiKey;
        }

        public async Task<string> AnswerAndQuestion(string question, int token)
        {

            string answer = string.Empty;

            var openai = new OpenAIAPI(ApiKey);
            CompletionRequest completion = new CompletionRequest();

            completion.Prompt = question;
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = token;

            CompletionResult result = await openai.Completions.CreateCompletionAsync(completion);

            if (result != null)
            {
                foreach (var item in result.Completions)
                {
                    answer += item.Text;
                }
            }

            return answer;

        }

        public async Task<string> Code(string question, int token)
        {

            string answer = string.Empty;

            var openai = new OpenAIAPI(ApiKey);
            CompletionRequest completion = new CompletionRequest();

            completion.Prompt = question;
            completion.Model = OpenAI_API.Models.Model.DavinciCode;
            completion.MaxTokens = token;

            CompletionResult result = await openai.Completions.CreateCompletionAsync(completion);

            if (result != null)
            {
                foreach (var item in result.Completions)
                {
                    answer += item.Text;
                }
            }

            return answer;

        }


        public async Task<string> GenerateImage(string requirement)
        {

            string answer = string.Empty;

            var openai = new OpenAIAPI(ApiKey);
            CompletionRequest completion = new CompletionRequest();

            completion.Prompt = requirement;
            completion.Model = OpenAI_API.Models.Model.DavinciCode;
            completion.NumChoicesPerPrompt = 1;

            CompletionResult result = await openai.Completions.CreateCompletionAsync(completion);

            if (result != null)
            {
                foreach (var item in result.Completions)
                {
                    answer += item.Text;
                }
            }

            return answer;

        }


        public async Task<string> HttpClientAnswerAndQuestion(string question, int token)
        {
            string model = "text-ada-001";
            string prompt = "What is the capital of France?";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiKey);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/engines/" + model + "/jobs");
            request.Content = new StringContent(JsonConvert.SerializeObject(new
            {
                prompt = prompt,
                max_tokens = 100,
                n = 1,
                stop = "",
                temperature = 0.5,
            }), System.Text.Encoding.UTF8, "application/json");

            var response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                dynamic responseJson = JsonConvert.DeserializeObject(responseString);
                Console.WriteLine("Response: " + responseJson.choices[0].text);
            }
            else
            {
                Console.WriteLine("Request failed with status code: " + response.StatusCode);
            }

            return "";
        }
    }

}
