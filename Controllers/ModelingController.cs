using api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelingController : ControllerBase
    {
        private readonly ILogger<ModelingController> _logger;

        public ModelingController(ILogger<ModelingController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pessoa))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPessoa([FromQuery] int id)
        {
            _logger.LogInformation("Obter Pessoa: {0}", id);
            
            var pessoa = new Pessoa
            {
                Id = id,
                Nome = "Nome",
                Endereco = new Endereco(),
                Ativo = true
            };

            return Ok(pessoa);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Pessoa))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePessoa([FromBody] Pessoa pessoa)
        {
            var pessoaString = JsonSerializer.Serialize(pessoa);
            _logger.LogInformation($"Criar Pessoa: {pessoaString}");
            var uri = $"{Request.Scheme}://{Request.Host}/modeling/get?id={1}";
            return Created(new Uri(uri), pessoa);
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pessoa))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePessoa([FromBody] Pessoa pessoa)
        {
            var pessoaString = JsonSerializer.Serialize(pessoa);
            _logger.LogInformation($"Atualizer Pessoa: {pessoaString}");
            return StatusCode(StatusCodes.Status200OK, pessoaString);
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePessoa([FromQuery] int id)
        {
            _logger.LogInformation("Deletar Pessoa: {0}", id);
            return StatusCode(StatusCodes.Status200OK, id.ToString());
        }
    }
}