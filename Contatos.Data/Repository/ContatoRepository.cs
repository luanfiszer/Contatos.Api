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
        public Task Adicionar(Contato contato)
        {
            _context.Contatos.Add(contato);
            return Task.CompletedTask;
        }

        public Task AtualizarAsync(Contato contato)
        {
            _context.Contatos.Update(contato);
            return Task.CompletedTask;
        }

        public async Task<Contato> ObterPorIdAsync(Guid id)
        {
            var contato = await _context.Contatos
                .FirstOrDefaultAsync(c => c.Id == id);

            return contato;
        }

        public async Task<IEnumerable<Contato>> ObterTodosAsync()
        {
            var contatos = await _context.Contatos
                .OrderBy(c => c.Nome)
                .AsNoTracking()
                .ToListAsync();

            return contatos;
        }

        public Task RemoverAsync(Contato contato)
        {
            _context.Contatos.Remove(contato);
            return Task.CompletedTask;
        }

        public async Task SalvarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
