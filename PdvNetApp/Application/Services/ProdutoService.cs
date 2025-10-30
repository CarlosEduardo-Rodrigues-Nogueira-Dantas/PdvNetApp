using PdvNetApp.Domain.Entities;
using PdvNetApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdvNetApp.Application.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repo;
        public ProdutoService(IProdutoRepository repo) => _repo = repo;
        public Task<List<Produto>> ListarAsync() => _repo.GetAllAsync();
        public Task CriarAsync(Produto produto) => _repo.AddAsync(produto);
        public Task AtualizarAsync(Produto produto) => _repo.UpdateAsync(produto);
        public Task ExcluirAsync(int id) => _repo.DeleteAsync(id);
    }
}