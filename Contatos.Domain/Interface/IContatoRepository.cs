using Contatos.Domain.Entity;

namespace Contatos.Domain.Interface
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> ObterAtivosAsync();
        Task<Contato?> ObterAtivoPorIdAsync(Guid id);
        Task AdicionarAsync(Contato contato);
        Task AtualizarAsync(Contato contato);
        Task RemoverAsync(Guid id);
        Task SalvarAsync();
    }
}
