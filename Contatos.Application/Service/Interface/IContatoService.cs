using Contatos.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Application.Service.Interface
{
    public interface IContatoService
    {
        Task<IEnumerable<ContatoDto>> ObterTodosAsync();
        Task<ContatoDto> ObterPorIdAsync(Guid id);
        Task<ContatoDto> CriarAsync(ContatoCreateDto dto);
        Task AtualizarAsync(Guid id, ContatoUpdateDto dto);
        Task RemoverAsync(Guid id);
        Task AlterarStatusAsync(Guid id);
    }
}
