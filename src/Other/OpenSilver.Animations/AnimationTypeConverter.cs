using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using static System.ComponentModel.TypeConverter;
using System.Windows.Input;
using Microsoft.Expression.Media.Effects;

namespace OpenSilver.Animations
{
    public class AnimationTypeConverter : TypeConverter
    {
        private StandardValuesCollection _standardValues;

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof(string);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                string text = s.Trim();

                return text switch
                {
                    "FadeAndScale" => new FadeAndScale(),
                    _ => throw new FormatException($"'{value}' is not a valid token.")
                };
            }

            throw GetConvertFromException(value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType is null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            if (value is IAnimationType animationType)
            {
                return animationType.ToString();
            }
            else
            {
                return string.Empty;
            }

            throw GetConvertToException(value, destinationType);
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (_standardValues is null)
            {
                _standardValues = new StandardValuesCollection(new[]
                {
                    new FadeAndScale()
                });
            }

            return _standardValues;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
    }
}
