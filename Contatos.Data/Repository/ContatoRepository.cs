using Contatos.Data.Context;
using Contatos.Domain.Entity;
using Contatos.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Data.Repository
{
    public class ContatoRepository : IContatoRepository
    {

        private readonly AppDbContext _context;

        public ContatoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AdicionarAsync(Contato contato)
        {
            await _context.Contato.AddAsync(contato);
        }

        public Task AtualizarAsync(Contato contato)
        {
            _context.Contato.Update(contato);
            return Task.CompletedTask;
        }

        public async Task<Contato?> ObterAtivoPorIdAsync(Guid id)
        {
            var contato = await _context.Contato
                .Where(c => c.Ativo)
                .FirstOrDefaultAsync(c => c.Id == id);

            return contato;
        }

        public async Task<IEnumerable<Contato>> ObterAtivosAsync()
        {
            var contatos = await _context.Contato
                .Where(c => c.Ativo)
                .OrderBy(c => c.Nome)
                .AsNoTracking()
                .ToListAsync();

            return contatos;
        }

        public async Task RemoverAsync(Guid id)
        {
            var contato = await ObterAtivoPorIdAsync(id);
            if (contato != null)
                _context.Contato.Remove(contato);
        }

        public async Task SalvarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
