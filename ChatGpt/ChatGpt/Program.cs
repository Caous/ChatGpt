// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using OpenAI_API.Completions;
using OpenAI_API;

Console.WriteLine("Test Chat Gpt");


string apiKey = "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";
//string model = "text-ada-001";
//string prompt = "What is the capital of France?";

//var client = new HttpClient();
//client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);

//var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/engines/" + model + "/jobs");
//request.Content = new StringContent(JsonConvert.SerializeObject(new
//{
//    prompt = prompt,
//    max_tokens = 100,
//    n = 1,
//    stop = "",
//    temperature = 0.5,
//}), System.Text.Encoding.UTF8, "application/json");

//var response = client.SendAsync(request).Result;
//if (response.IsSuccessStatusCode)
//{
//    var responseString = response.Content.ReadAsStringAsync().Result;
//    dynamic responseJson = JsonConvert.DeserializeObject(responseString);
//    Console.WriteLine("Response: " + responseJson.choices[0].text);
//}
//else
//{
//    Console.WriteLine("Request failed with status code: " + response.StatusCode);
//}

//string OutPutResult = "";
//var openai = new OpenAIAPI(apiKey);
//CompletionRequest completionRequest = new CompletionRequest();
//completionRequest.Prompt = "Me gere uma classe em C# de calculadora";
//completionRequest.Model = OpenAI_API.Models.Model.DavinciText;

//var completions = openai.Completions.CreateCompletionAsync(completionRequest);

//foreach (var completion in completions.Result.Completions)
//{
//    OutPutResult += completion.Text;
//}

//Console.WriteLine(OutPutResult);

//your OpenAI API key
string answer = string.Empty;
var openai = new OpenAIAPI(apiKey);
CompletionRequest completion = new CompletionRequest();
completion.Prompt = "Gere para mim uma classe em python de uma calculadora";
completion.Model = "code-davinci-002";
completion.MaxTokens = 100;
var result = openai.Completions.CreateCompletionAsync(completion);
if (result != null)
{
    foreach (var item in result.Result.Completions)
    {
        Console.WriteLine(item.Text);
    }
}
