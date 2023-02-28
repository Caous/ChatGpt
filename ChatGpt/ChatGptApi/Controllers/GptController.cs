using ChatGptApi.Service;
using Microsoft.AspNetCore.Mvc;

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
