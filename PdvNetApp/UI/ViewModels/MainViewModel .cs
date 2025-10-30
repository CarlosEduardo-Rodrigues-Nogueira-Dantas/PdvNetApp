using PdvNet.UI.ViewModels;
using PdvNetApp.Application.Services;
using PdvNetApp.Domain.Entities;
using PdvNetApp.UI;
using PdvNetApp.UI.View;
using PdvNetApp.UIWPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PdvNetApp.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ProdutoService _service;

        public ObservableCollection<Produto> Produtos { get; } = new();

        private Produto? _selecionado;
        public Produto? Selecionado
        {
            get => _selecionado;
            set
            {
                _selecionado = value;
                OnPropertyChanged();

                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand NovoCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand ExcluirCommand { get; }

        public ICommand AbrirDashboardCommand { get; }

        public MainViewModel(ProdutoService service)
        {
            _service = service;

            NovoCommand = new RelayCommand(_ => Novo());
            EditarCommand = new RelayCommand(_ => Editar(), _ => Selecionado != null);
            ExcluirCommand = new RelayCommand(async _ => await Excluir(), _ => Selecionado != null);
            AbrirDashboardCommand = new RelayCommand(_ => AbrirDashboard());

            _ = Carregar();
        }

        private async Task Carregar()
        {
            Produtos.Clear();
            foreach (var p in await _service.ListarAsync())
                Produtos.Add(p);
        }

        private void Novo()
        {
            var vm = new ProdutoFormViewModel(new Produto(), async p =>
            {
                await _service.CriarAsync(p);
                await Carregar();
            });

            new ProdutoFormWindow { DataContext = vm }.ShowDialog();
        }

        private void Editar()
        {
            if (Selecionado == null) return;

            var vm = new ProdutoFormViewModel(new Produto
            {
                Id = Selecionado.Id,
                Nome = Selecionado.Nome,
                Preco = Selecionado.Preco,
                Quantidade = Selecionado.Quantidade
            }, async p =>
            {
                await _service.AtualizarAsync(p);
                await Carregar();
            });

            new ProdutoFormWindow { DataContext = vm }.ShowDialog();
        }

        private async Task Excluir()
        {
            if (Selecionado == null) return;

            if (MessageBox.Show($"Excluir {Selecionado.Nome}?", "Confirmação", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                await _service.ExcluirAsync(Selecionado.Id);
                Produtos.Remove(Selecionado);
            }
        }

        private void AbrirDashboard()  
        {
            var vm = new DashboardViewModel(_service);
            var dashboard = new DashboardWindow
            {
                DataContext = vm,
            };
            dashboard.ShowDialog();
        }

    }
}