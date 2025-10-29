using System;
using System.Globalization;
using System.Windows.Data;

namespace PdvNetApp.UI.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal dec)
                return dec.ToString("N2", new CultureInfo("pt-BR"));
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (decimal.TryParse(value?.ToString(), NumberStyles.Any, new CultureInfo("pt-BR"), out var result))
                return result;

            return 0m;
        }
    }
}
