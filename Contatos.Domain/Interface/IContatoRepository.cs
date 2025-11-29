using Contatos.Domain.Entity;
using System.Linq.Expressions;

namespace Contatos.Domain.Interface
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> ObterTodosAsync(Expression<Func<Contato, bool>> filtro);
        Task<Contato?> ObterAsync(Expression<Func<Contato, bool>> filtro);
        Task Adicionar(Contato contato);
        Task AtualizarAsync(Contato contato);
        Task RemoverAsync(Contato contato);
        Task SalvarAsync();
    }
}
