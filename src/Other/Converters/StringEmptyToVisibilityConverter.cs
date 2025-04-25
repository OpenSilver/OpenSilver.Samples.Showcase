using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace OpenSilver.Samples.Showcase
{
    public class StringEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string && !string.IsNullOrEmpty((string)value))
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
