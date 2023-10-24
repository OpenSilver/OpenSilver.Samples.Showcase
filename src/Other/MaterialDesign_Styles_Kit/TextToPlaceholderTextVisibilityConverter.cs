using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace MaterialDesign_Styles_Kit
{
    public class TextToPlaceholderTextVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (string.IsNullOrEmpty((string)value))
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
            throw new InvalidOperationException("The IValueConverter expects a value of type String.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
