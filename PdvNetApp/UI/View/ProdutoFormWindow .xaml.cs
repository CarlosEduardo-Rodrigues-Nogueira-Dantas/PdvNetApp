using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PdvNetApp.UI.View
{
    /// <summary>
    /// Interaction logic for ProdutoFormWindow.xaml
    /// </summary>
    public partial class ProdutoFormWindow : Window
    {
        private bool _isUpdatingText;

        public ProdutoFormWindow()
        {
            InitializeComponent();
        }

        // Permite apenas números, vírgula e ponto
        private void PrecoTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9.,]+$");
        }

        // Permite apagar (backspace/delete)
        private void PrecoTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab ||
                e.Key == Key.Left || e.Key == Key.Right)
                e.Handled = false;
        }

        // Formata automaticamente o número enquanto digita
        private void PrecoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdatingText) return;
            _isUpdatingText = true;

            var textBox = sender as TextBox;
            var text = textBox.Text;

            // Remove tudo que não for número
            var digits = Regex.Replace(text, @"[^\d]", "");

            if (decimal.TryParse(digits, out decimal valor))
            {
                valor /= 100; // transforma "1234" em "12,34"
                textBox.Text = valor.ToString("N2", new CultureInfo("pt-BR"));
                textBox.CaretIndex = textBox.Text.Length;
            }

            _isUpdatingText = false;
        }
    }
}
