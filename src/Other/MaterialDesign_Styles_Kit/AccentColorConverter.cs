using System;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
#else
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
#endif

namespace MaterialDesign_Styles_Kit
{
    public class AccentColorConverter : IValueConverter
    {
#if SLMIGRATION
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
#else
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            if (value is SolidColorBrush)
            {
                Color oldColor = ((SolidColorBrush)value).Color;
                Byte newR = oldColor.R > 40 ? (Byte)(oldColor.R - 40) : (Byte)0;
                Byte newG = oldColor.G > 40 ? (Byte)(oldColor.G - 40) : (Byte)0;
                Byte newB = oldColor.B > 40 ? (Byte)(oldColor.B - 40) : (Byte)0;
                Color newColor = Color.FromArgb(oldColor.A, newR, newG, newB);
                return new SolidColorBrush(newColor);
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
