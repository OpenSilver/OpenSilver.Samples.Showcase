using System;
#if SLMIGRATION
using System.Windows;
using System.Windows.Data;
using System.Globalization;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#endif

namespace MaterialDesign_Styles_Kit
{
    public class TextToPlaceholderTextVisibilityConverter : IValueConverter
    {
#if SLMIGRATION
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
#else
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            if (value is string)
            {
                if (string.IsNullOrEmpty((string)value))
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
            throw new InvalidOperationException("The IValueConverter expects a value of type String.");
        }

#if SLMIGRATION
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
#else
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#endif
        {
            throw new NotImplementedException();
        }
    }
}
