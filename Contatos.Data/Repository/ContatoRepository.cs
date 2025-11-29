using Contatos.Data.Context;
using Contatos.Domain.Entity;
using Contatos.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public async Task<IEnumerable<Contato>> ObterTodosAsync(Expression<Func<Contato, bool>> filtro = null)
        {
            IQueryable<Contato> query = _context.Contatos;

            if (filtro != null)
                query = query.Where(filtro);

            return await query
                .OrderBy(c => c.Nome)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Contato> ObterAsync(Expression<Func<Contato, bool>> filtro)
        {
            return await _context.Contatos
                .Where(filtro)
                .FirstOrDefaultAsync();
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
