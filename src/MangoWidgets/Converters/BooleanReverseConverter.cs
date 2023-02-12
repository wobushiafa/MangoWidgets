using System;
using System.Globalization;
using System.Windows.Data;

namespace MangoWidgets.Converters
{
    public class BooleanReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean.TryParse(value?.ToString(),out bool result);
            return !result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean.TryParse(value?.ToString(), out bool result);
            return !result;
        }
    }
}
