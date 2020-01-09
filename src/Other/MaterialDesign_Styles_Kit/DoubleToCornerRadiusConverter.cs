using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MaterialDesign_Styles_Kit
{
    public class DoubleToCornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double)
            {
                return new CornerRadius((double)value);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
