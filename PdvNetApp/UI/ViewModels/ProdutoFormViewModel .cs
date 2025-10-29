using PdvNetApp.Domain.Entities;
using PdvNetApp.UI.ViewModels;
using PdvNetApp.UIWPF.Commands;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PdvNet.UI.ViewModels
{
    public class ProdutoFormViewModel : ViewModelBase
    {
        public Produto Produto { get; set; }
        public bool IsEdit { get; }
        private readonly Func<Produto, Task> _onSave;

        public ICommand SalvarCommand { get; }

        public ProdutoFormViewModel(Produto produto, Func<Produto, Task> onSave)
        {
            Produto = produto;
            _onSave = onSave;
            SalvarCommand = new RelayCommand(async w => await SalvarAsync(w));
        }

        private async Task SalvarAsync(object? window)
        {
            try
            {
                // 🧩 Validações de negócio
                if (string.IsNullOrWhiteSpace(Produto.Nome))
                    throw new ValidationException("O nome é obrigatório.");

                if (Produto.Preco <= 0)
                    throw new ValidationException("O preço deve ser maior que zero.");

                if (Produto.Quantidade <= 0)
                    throw new ValidationException("A quantidade deve ser maior que zero.");

                // 🔹 Se passou na validação, salva no banco
                await _onSave(Produto);

                MessageBox.Show("Produto salvo com sucesso!", "Sucesso",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                (window as Window)?.Close();
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message, "Validação",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar o produto: {ex.Message}", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
