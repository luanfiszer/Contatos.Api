using Contatos.Api.Results;
using Contatos.Application.Dto;
using Contatos.Application.DTO;
using Contatos.Application.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _service;

        public ContatoController(IContatoService service)
        {
            _service = service;
        }

        [HttpGet("ativos")]
        [ProducesResponseType(typeof(IEnumerable<ContatoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterAtivos()
        {
            var contatos = await _service.ObterTodosAsync();
            return Ok(Result<IEnumerable<ContatoDto>>.Ok(contatos));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ContatoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var dto = await _service.ObterPorIdAsync(id);
            return Ok(Result<ContatoDto>.Ok(dto));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContatoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] ContatoCreateDto dto)
        {
            var contato = await _service.CriarAsync(dto);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = contato.Id },
                Result<ContatoDto>.Ok(contato, "Contato criado com sucesso")
            );
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] ContatoUpdateDto dto)
        {
            await _service.AtualizarAsync(id, dto);
            return Ok(Result.Ok("Contato atualizado com sucesso"));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remover(Guid id)
        {
            await _service.RemoverAsync(id);
            return Ok(Result.Ok("Contato removido com sucesso"));
        }

        [HttpPatch("{id:guid}/ativo")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchAtivo(Guid id, [FromBody] ContatoPatchAtivoDto dto)
        {
            await _service.AlterarStatusAsync(id, dto.Ativo);
            return Ok(Result.Ok("Status de ativo atualizado com sucesso"));
        }

    }
}
