using Contatos.Api.Results;
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
        Task<Result<IEnumerable<ContatoDto>>> ListarAsync();
        Task<Result<ContatoDto>> ObterPorIdAsync(Guid id);
        Task<Result<ContatoDto>> CriarAsync(ContatoRequestDto dto);
        Task<Result> AtualizarAsync(Guid id, ContatoRequestDto dto);
        Task<Result> RemoverAsync(Guid id);
        Task<Result> AlterarStatusAsync(Guid id);
    }
}
