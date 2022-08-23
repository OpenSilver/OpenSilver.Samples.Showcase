using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if SLMIGRATION
using System.Windows;
using System.Windows.Data;
using System.Globalization;
#else
using Windows.UI.Xaml.Data;
#endif

namespace OpenSilver.Samples.Showcase
{
    public class KilometersToMilesConverter : IValueConverter
    {
#if SLMIGRATION
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
#else
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            int kilometers;
            if (value != null && int.TryParse(value.ToString(), out kilometers))
            {
                return (int)(kilometers * 0.62137119);
            }
            else
                return "";
        }

#if SLMIGRATION
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
#else
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#endif
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
