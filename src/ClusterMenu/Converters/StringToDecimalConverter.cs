using System;
using System.Globalization;
using System.Windows.Data;

namespace ClusterMenu.Converters {
    
    public class StringToDecimalConverter : IValueConverter {
        
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is null) return decimal.Zero;
            var str = value.ToString();
            if (string.IsNullOrWhiteSpace(str)) return decimal.Zero;
            return decimal.Parse(str, CultureInfo.CurrentCulture);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is null) return string.Empty;
            return value.ToString();
        }

    }
}
