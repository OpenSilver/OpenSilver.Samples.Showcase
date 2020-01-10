using System;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#endif

namespace MaterialDesign_Styles_Kit
{
    public class DoubleToCornerRadiusConverter : IValueConverter
    {
#if SLMIGRATION
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
#else
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            if (value is double)
            {
                return new CornerRadius((double)value);
            }
            return value;
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
