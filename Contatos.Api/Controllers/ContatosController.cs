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

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ContatoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar()
        {
            var contatos = await _contatoService.ListarAsync();
            return Ok(Result<IEnumerable<ContatoDto>>.Ok(contatos));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var dto = await _contatoService.ObterPorIdAsync(id);
            return Ok(Result<ContatoDto>.Ok(dto));
        }

        [HttpPost()]
        [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] ContatoCreateDto dto)
        {
            var contato = await _contatoService.CriarAsync(dto);

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
            await _contatoService.AtualizarAsync(id, dto);
            return Ok(Result.Ok("Contato atualizado com sucesso"));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remover(Guid id)
        {
            await _contatoService.RemoverAsync(id);
            return Ok(Result.Ok("Contato removido com sucesso"));
        }

        [HttpPatch("Ativos/{id:guid}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlterarStatus(Guid id)
        {
            await _contatoService.AlterarStatusAsync(id);
            return Ok(Result.Ok("Status atualizado com sucesso"));
        }

    }
}
