using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace OpenSilver.Samples.Showcase
{
    public class EnumToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter switch
            {
                Type type => GetValues(type),
                DependencyProperty dp => GetValues(dp.PropertyType),
                _ => null,
            };
        }

        private static object GetValues(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            var enumType = underlyingType ?? type;
            if (enumType.IsEnum)
            {
                var values = Enum.GetValues(enumType);
                return underlyingType != null ? new object[] { null }.Concat(values.Cast<object>()) : values;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
