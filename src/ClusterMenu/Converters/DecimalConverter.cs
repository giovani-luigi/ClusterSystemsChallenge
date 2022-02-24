using System;
using System.Globalization;
using System.Windows.Data;

namespace ClusterMenu.Converters {
    
    public class DecimalConverter : IValueConverter {
        
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is null) return string.Empty;
            return value.ToString();
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is null) return decimal.Zero;
            var str = value.ToString();
            if (string.IsNullOrWhiteSpace(str)) return decimal.Zero;
            try {
                return decimal.Parse(str, CultureInfo.CurrentCulture);
            } catch (Exception e) {
                return decimal.Zero;
            }
        }

    }
}
