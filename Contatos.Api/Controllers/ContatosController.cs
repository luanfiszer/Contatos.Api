using Contatos.Api.Results;
using Contatos.Application.Dto;
using Contatos.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatosController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ContatoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar()
        {
            var result = await _contatoService.ListarAsync();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var result = await _contatoService.ObterPorIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] ContatoRequestDto dto)
        {
            var result = await _contatoService.CriarAsync(dto);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] ContatoRequestDto dto)
        {
            var result = await _contatoService.AtualizarAsync(id, dto);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remover([FromRoute]Guid id)
        {
            var result = await _contatoService.RemoverAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPatch("Status/{id:guid}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlterarStatus([FromRoute] Guid id)
        {
            var result = await _contatoService.AlterarStatusAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

    }
}
