Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media

Namespace OpenSilver.Samples.Showcase
    Public NotInheritable Class MenuIconBrushConverter
        Implements IMultiValueConverter

        Public Function Convert(values As Object(), targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim tvi = TryCast(values(0), TreeViewItem)
            'Dim isSelected = TryCast(values(1), Boolean?)

            'If tvi Is Nothing Then
            '    Return DependencyProperty.UnsetValue
            'End If

            'If isSelected.HasValue AndAlso isSelected.Value Then
            '    Dim brush = TryCast(tvi.TryFindResource("Theme_TextOnPrimaryBrush"), Brush)
            '    Return If(brush, Brushes.White)
            'End If

            'Dim pageInfo = TryCast(tvi.DataContext, PageInfo)
            'Return If(pageInfo?.IconBrush, DependencyProperty.UnsetValue)
            Return Nothing
        End Function

        Public Function ConvertBack(value As Object, targetTypes As Type(), parameter As Object, culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
End Namespace
