using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace OpenSilver.Samples.Showcase
{
    public sealed class MenuIconBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
                              object parameter, CultureInfo culture)
        {
            var tvi = values[0] as TreeViewItem; // ancestor container
            var isSelected = values[1] as bool?; // its IsSelected flag

            if (tvi == null) return DependencyProperty.UnsetValue;

            if (isSelected == true)
            {
                // same brush the visual state uses:
                return tvi.TryFindResource("Theme_TextOnPrimaryBrush") as Brush
                       ?? Brushes.White;   // safe fallback
            }

            // normal state – take the page’s own colour
            return (tvi.DataContext as PageInfo)?.IconBrush
                   ?? DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
                                    object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}