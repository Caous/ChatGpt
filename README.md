### <h2>ChatGpt 🤖
  <h5> Projeto com finalidade de mostrar integrações com o ChatGpt usando Framework e WebClient  <img src="https://media.tenor.com/J8LIZC2OVOoAAAAC/rob%C3%B4.gif" width="30"> 
</em></p></h5>
  
  </br>
  
### <h2>Fala Dev, seja muito bem-vindo
   Projeto foi construído com intuito de mostrar como podemos utilizar a inteligência artificial a nosso favor. Se vamos ser substituídos pelas IA? Gosto de falar a seguinte frase: <b>Uma máquina pode fazer o trabalho humano melhor do que o próprio humano, mas um humano com uma máquina pode fazer melhor que a máquina sozinha.</b> Espero que encontre o que procura <img src="https://media.giphy.com/media/WUlplcMpOCEmTGBtBW/giphy.gif" width="30"> 
</em></p></h5>

<img src="https://sujeitoprogramador.com/wp-content/uploads/2019/03/19AIcover-illo-master1050-v5.gif" width="400" height="300"/>
<br>



### <h2>ChatGpt <a href="https://openai.com/blog/chatgpt" target="_blank"><img alt="Serilog" src="https://img.shields.io/badge/Chat-blue?style=flat&logo=google-chrome"></a>

ChatGpt ou IA como muitos gostam de falar é uma solução muito fácil e sucinta de interação com o usuário, como já fiz seu nome Chat, ele permite que possamos nos comunicar via um chat com a IA, assim podendo fazer diversas perguntas, pedir para analisar códigos de programação até mesmo gerar imagens para nós e como resultado ele responde oque pedirmos a ele via chat (bate papo), cada contexto que damos a ele, são mais informações para a IA aprender a nos responder cada vez de forma mais precisa e também podemos treinar ele para responder como desejamos.
 
Lembrando que pode ser usado também para gerar imagens e corrigir código ou até mesmo gerar códigos HTML e afins para você <b>INDIFERENTE DA LINGUAGEM DE PROMAÇÃO</b>, ou seja, pode ser aplicado em qualquer lugar. Mas fica um <b>Ponto de Atenção</b> para vocês, ele nem sempre de primeira vez vai lhe dar oque deseja, então passe bastante detalhes a ele.

Vou mostrar a vocês duas maneiras de integrar o <b>ChatGpt ao C#</b> utilizando <b>HttpClient ou Framework's</b> que estão sendo desenvolvidos hoje.
 
 ### <h2>Documentação e Framework <a href="https://openai.com/blog/chatgpt" target="_blank"><img alt="ChatGpt" src="https://img.shields.io/badge/Chat-blue?style=flat&logo=google-chrome"></a>
 
 Para documentação de integração via HttpClient utilize a própria documentação do ChatGpt utilize esse link como apoio: https://platform.openai.com/docs/api-reference/completions/create
 
 E também temos o <b>OpenIa</b> podendo utilizar a documentação e via Nuget Package: https://github.com/OkGoDoIt/OpenAI-API-dotnet
 
 
Legal né? Mas agora a pergunta é como posso implementar? Abaixo dou um exemplo de caso de uso de chamada e implementação via API assim você pode deixar essa integração disponível para quem quiser consumir o ChatGpt.

</br></br>

### <h2> Criação de Classes

Vamos criar uma controller que irá chamar os métodos de service que contem implementação com o ChatGpt
```C#
   namespace ChatGptApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GptController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public GptController(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }

        [HttpGet("GetAnswerAndQuestion")]
        public async Task<IActionResult> GetAnswerAndQuestion(string request, int token)
        {
            if (string.IsNullOrEmpty(request))
                return BadRequest("Please fill question");

            ChatGPTService chatGPT = new ChatGPTService(_configuration["ApiKey"], _clientFactory);

            return Ok(await chatGPT.AnswerAndQuestion(request, token));

        }

        [HttpGet("GetCode")]
        public async Task<IActionResult> GetCode(string request, int token)
        {
            if (string.IsNullOrEmpty(request))
                return BadRequest("Please fill question");


            ChatGPTService chatGPT = new ChatGPTService(_configuration["ApiKey"], _clientFactory);

            return Ok(await chatGPT.Code(request, token));

        }


        [HttpGet("HttpClienteAnswerAndQuestion")]
        public async Task<IActionResult> HttpClienteAnswerAndQuestion(string request, int token)
        {
            if (string.IsNullOrEmpty(request))
                return BadRequest("Please fill question");

            ChatGPTService chatGPT = new ChatGPTService(_configuration["ApiKey"], _clientFactory);

            return Ok(await chatGPT.HttpClientAnswerAndQuestion(request, token));

        }

        [HttpGet("HttpClienteGenerateImage")]
        public async Task<IActionResult> HttpClienteGenerateImage(string request, string size)
        {
            if (string.IsNullOrEmpty(request))
                return BadRequest("Please fill question");
            
            ChatGPTService chatGPT = new ChatGPTService(_configuration["ApiKey"], _clientFactory);

            return Ok(await chatGPT.GenerateImagem(request, size));

        }
    }
}
```

Service com implementação ao ChatGpt
```C#
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
```
</br></br>

### <h5> [IDE Utilizada]</h5>
![VisualStudio](https://img.shields.io/badge/Visual_Studio_2019-000000?style=for-the-badge&logo=visual%20studio&logoColor=purple)

### <h5> [Linguagem Programação Utilizada]</h5>
![C#](https://img.shields.io/badge/C%23-000000?style=for-the-badge&logo=c-sharp&logoColor=purple)


### <h5> [Web Utilizado🌐]</h5>
![HTML5](https://img.shields.io/badge/-HTML5-000000?style=for-the-badge&logo=HTML5)
![CSS3](https://img.shields.io/badge/-CSS3-000000?style=for-the-badge&logo=CSS3)
![JavaScript](https://img.shields.io/badge/-JavaScript-000000?style=for-the-badge&logo=javascript)



<p align="center">
  <i>🤝🏻 Vamos nos conectar!</i>

  <p align="center">
    <a href="https://www.linkedin.com/in/gusta-nascimento/" alt="Linkedin"><img src="https://github.com/nitish-awasthi/nitish-awasthi/blob/master/174857.png" height="30" width="30"></a>
    <a href="https://www.instagram.com/gusta.nascimento/" alt="Instagram"><img src="https://github.com/nitish-awasthi/nitish-awasthi/blob/master/instagram-logo-png-transparent-background-hd-3.png" height="30" width="30"></a>
    <a href="mailto:caous.g@gmail.com" alt="E-mail"><img src="https://github.com/nitish-awasthi/nitish-awasthi/blob/master/gmail-512.webp" height="30" width="30"></a>   
  </p>
