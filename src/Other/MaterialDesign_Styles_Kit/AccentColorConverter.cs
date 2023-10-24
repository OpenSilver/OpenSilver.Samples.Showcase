using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;

namespace MaterialDesign_Styles_Kit
{
    public class AccentColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
