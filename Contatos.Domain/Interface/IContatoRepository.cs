using Contatos.Domain.Entity;

namespace Contatos.Domain.Interface
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> ObterTodosAsync();
        Task<Contato> ObterPorIdAsync(Guid id);
        Task Adicionar(Contato contato);
        Task AtualizarAsync(Contato contato);
        Task RemoverAsync(Contato contato);
        Task SalvarAsync();
    }
}
