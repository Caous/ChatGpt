using OpenAI_API.Completions;
using OpenAI_API;

namespace ChatGptApi.Service
{
    public class TrainingChatGpt
    {
        private readonly string _apiKey;
        private OpenAIAPI _openai;

        public TrainingChatGpt(string ApiKey)
        {
            _apiKey = ApiKey;
        }
        public async Task<bool> TrainingBot()
        {

            string answer = string.Empty;

            _openai = new OpenAIAPI(_apiKey);
            CompletionRequest completion = new CompletionRequest();

            completion.Prompt = "Quero treinar você ChatGpt";
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 2000;

            Console.WriteLine(await RequestGpt(answer, _openai, completion));

            completion.Prompt = "Você vai apartir de agora me dar respostas, para resolver problemas básicos de suporte de computadores, roteadores e infraestrutura, como modem de internet e afim";
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 2000;

            Console.WriteLine(await RequestGpt(answer, _openai, completion));

            completion.Prompt = "Quero que me traga sempre 5 itens de possíveis soluções, trazendo textos mais simples possíveis, para usuário leigos, lembrando que isto não é uma pergunta, apenas quando for pergunta retornar os 5 itens";
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 2000;

            Console.WriteLine(await RequestGpt(answer, _openai, completion));

            completion.Prompt = "Quando for dar respostas para mim pode me dirigir como Senhor ou Senhora, por favor";
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 2000;

            Console.WriteLine(await RequestGpt(answer, _openai, completion));


            return true;
        }
        public async Task<string> RequestGpt(string answer, OpenAIAPI openai, CompletionRequest completion)
        {
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

        public OpenAIAPI returnTraning()
        {
            return _openai;
        }
    }
}
