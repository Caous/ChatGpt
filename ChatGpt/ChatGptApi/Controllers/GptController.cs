using ChatGptApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChatGptApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GptController : Controller
    {
        private readonly IConfiguration _configuration;

        public GptController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetAnswerAndQuestion")]
        public async Task<IActionResult> GetAnswerAndQuestion(string request, int token)
        {
            if (string.IsNullOrEmpty(request))
                return BadRequest("Please fill question");

            ChatGPTService chatGPTService = new ChatGPTService(_configuration["ApiKey"]);

            return Ok(await chatGPTService.AnswerAndQuestion(request, token));

        }

        [HttpGet("GetCode")]
        public async Task<IActionResult> GetCode(string request, int token)
        {
            if (string.IsNullOrEmpty(request))
                return BadRequest("Please fill question");

            ChatGPTService chatGPTService = new ChatGPTService(_configuration["ApiKey"]);

            return Ok(await chatGPTService.Code(request, token));

        }

        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(string request)
        {
            if (string.IsNullOrEmpty(request))
                return BadRequest("Please fill question");

            ChatGPTService chatGPTService = new ChatGPTService(_configuration["ApiKey"]);

            return Ok(await chatGPTService.GenerateImage(request));

        }

        [HttpGet("HttpClienteAnswerAndQuestion")]
        public async Task<IActionResult> HttpClienteAnswerAndQuestion(string request, int token)
        {
            if (string.IsNullOrEmpty(request))
                return BadRequest("Please fill question");

            ChatGPTService chatGPTService = new ChatGPTService(_configuration["ApiKey"]);

            return Ok(await chatGPTService.HttpClientAnswerAndQuestion(request, token));

        }
    }
}
