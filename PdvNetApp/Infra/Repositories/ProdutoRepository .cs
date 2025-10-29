using Microsoft.EntityFrameworkCore;
using PdvNetApp.Domain.Entities;
using PdvNetApp.Domain.Interfaces;
using PdvNetApp.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdvNetApp.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync() =>
            await _context.Produtos.AsNoTracking().ToListAsync();

        public async Task<Produto?> GetByIdAsync(int id) =>
            await _context.Produtos.FindAsync(id);

        public async Task AddAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Produtos.FindAsync(id);
            if (entity != null)
            {
                _context.Produtos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
