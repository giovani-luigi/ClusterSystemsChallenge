using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ClusterMenu.Converters {
    public class IdConverter : IValueConverter {
        
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is int i) {
                if (i < 0) return string.Empty;
                return value.ToString();
            }
            return string.Empty;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string s) {
                return int.Parse(s);
            }
            return 0;
        }

    }
}
