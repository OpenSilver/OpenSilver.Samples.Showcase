
/*===================================================================================
* 
*   Copyright (c) Userware/OpenSilver.net
*      
*   This file is part of OpenSilver (https://opensilver.net), which is
*   licensed under the MIT license: https://opensource.org/licenses/MIT
*   
*   IMPORTANT: Make sure to preserve this copyright notice on all copies of this code.
*  
\*====================================================================================*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using static System.ComponentModel.TypeConverter;
using System.Windows.Input;
using Microsoft.Expression.Media.Effects;
using OpenSilver.Animations.Easing;

namespace OpenSilver.Animations
{
    public class EasingTypeConverter : TypeConverter
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
                    "Linear" => new Linear(),
                    "CubicIn" => new CubicIn(),
                    "CubicOut" => new CubicOut(),
                    "CubicInOut" => new CubicInOut(),
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

            if (value is IEasingType easingType)
            {
                return easingType.ToString();
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
                _standardValues = new StandardValuesCollection(new IEasingType[]
                {
                    new Linear(),
                    new CubicIn(),
                    new CubicOut(),
                    new CubicInOut(),
                });
            }

            return _standardValues;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
    }
}
