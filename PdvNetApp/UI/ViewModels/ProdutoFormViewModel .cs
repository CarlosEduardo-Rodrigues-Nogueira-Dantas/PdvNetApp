using PdvNetApp.Domain.Entities;
using PdvNetApp.UI.ViewModels;
using PdvNetApp.UIWPF.Commands;
using System;
using System.Windows;
using System.Windows.Input;
namespace PdvNet.UI.ViewModels;

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
        await _onSave(Produto);
        (window as Window)?.Close();
    }
}
