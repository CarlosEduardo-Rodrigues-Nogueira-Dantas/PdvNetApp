using System;
using System.Globalization;
using System.Windows.Data;

namespace PdvNetApp.UI.Converters
{
    public class DecimalPtBrConverter : IValueConverter
    {
        private static readonly CultureInfo PtBr = CultureInfo.GetCultureInfo("pt-BR");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (value is decimal d) return d.ToString("N2", PtBr);
            if (value is double db) return db.ToString("N2", PtBr);
            try
            {
                var dec = System.Convert.ToDecimal(value);
                return dec.ToString("N2", PtBr);
            }
            catch
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (value as string)?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(s))
            {
                if (Nullable.GetUnderlyingType(targetType) != null) return null;
                return 0m;
            }

            if (decimal.TryParse(s, System.Globalization.NumberStyles.Number, PtBr, out var result))
                return result;

            throw new FormatException("Formato inválido. Use vírgula como separador decimal, ex: 1,23");
        }
    }
}