using PdvNetApp.Application.Services;
using PdvNetApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdvNetApp.UI.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly ProdutoService _produtoService;

    // 🔹 Dados expostos na tela
    private int _totalProdutos;
    public int TotalProdutos
    {
        get => _totalProdutos;
        set { _totalProdutos = value; OnPropertyChanged(); }
    }

    private decimal _valorTotalEstoque;
    public decimal ValorTotalEstoque
    {
        get => _valorTotalEstoque;
        set { _valorTotalEstoque = value; OnPropertyChanged(); }
    }

    public ObservableCollection<Produto> ProdutosBaixoEstoque { get; } = new();

    public DashboardViewModel(ProdutoService produtoService)
    {
        _produtoService = produtoService;
        _ = CarregarDadosAsync();
    }

    private async Task CarregarDadosAsync()
    {
        // 🔹 Busca todos os produtos do banco
        var produtos = (await _produtoService.ListarAsync()).ToList();

        // 🔹 Calcula os indicadores
        TotalProdutos = produtos.Count;
        ValorTotalEstoque = produtos.Sum(p => p.Preco * p.Quantidade);

        // 🔹 Lista produtos com quantidade < 5
        ProdutosBaixoEstoque.Clear();
        foreach (var p in produtos.Where(p => p.Quantidade < 5))
            ProdutosBaixoEstoque.Add(p);
    }
}
}
