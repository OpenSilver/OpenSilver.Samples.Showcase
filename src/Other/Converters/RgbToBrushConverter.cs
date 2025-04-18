using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace OpenSilver.Samples.Showcase;

/// <summary>
/// This class implements the IMultiValueConverter interface, which allows us to convert multiple values into a single value.
/// In this case, it converts three RGB (Red, Green, Blue) values into a Brush (a color that can be used for painting UI elements).
/// </summary>
public class RgbToBrushConverter : IMultiValueConverter
{
    // The Convert method is called to convert the source values (RGB) to the target value (a Brush).
    // It receives an array of values (object[]), which are the values passed from the XAML bindings.
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        // First, we check if we received exactly three values and that each value can be cast to a double.
        if (values.Length == 3
        && values[0] is double r // 'r' is the red component
        && values[1] is double g // 'g' is the green component
        && values[2] is double b) // 'b' is the blue component
        {
            // If the values are valid, we create a new SolidColorBrush using the RGB values.
            // We cast each double to a byte because the FromRgb method expects bytes.
            return new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
        }

        // If the values are not valid, return a default color (black) as a fallback.
        return new SolidColorBrush(Colors.Black); // Default color in case of error
    }

    // The ConvertBack method is used to convert a value back to its source values.
    // In this case, it's not implemented because we don't need to convert a Brush back to RGB values.
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
