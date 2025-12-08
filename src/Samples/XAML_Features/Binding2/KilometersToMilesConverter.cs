using System;
using System.Windows.Data;
using System.Globalization;

namespace OpenSilver.Samples.Showcase
{
    public class KilometersToMilesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int kilometers;
            if (value != null && int.TryParse(value.ToString(), out kilometers))
            {
                return (int)(kilometers * 0.62137119);
            }
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int miles;
            if (value != null && int.TryParse(value.ToString(), out miles))
            {
                return (int)(miles * 1.6093440);
            }
            else
                return "";
        }
    }
}
