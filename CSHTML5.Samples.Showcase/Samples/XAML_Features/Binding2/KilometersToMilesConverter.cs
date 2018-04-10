using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace CSHTML5.Samples.Showcase
{
    public class KilometersToMilesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int kilometers;
            if (value != null && int.TryParse(value.ToString(), out kilometers))
            {
                return (int)(kilometers * 0.62137119);
            }
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
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
