Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media

Namespace OpenSilver.Samples.Showcase
    Public NotInheritable Class MenuIconBrushConverter
        Implements IMultiValueConverter

        Public Function Convert(values As Object(), targetType As Type, parameter As Object, culture As CultureInfo) As Object _
            Implements IMultiValueConverter.Convert

            Dim tvi = TryCast(values(0), TreeViewItem) ' ancestor container
            Dim isSelected As Boolean? = Nothing
            If values.Length > 1 AndAlso values(1) IsNot Nothing AndAlso TypeOf values(1) Is Boolean Then
                isSelected = CType(values(1), Boolean)
            End If

            If tvi Is Nothing Then
                Return DependencyProperty.UnsetValue
            End If

            If isSelected.HasValue AndAlso isSelected.Value Then
                ' same brush the visual state uses:
                Dim brush = TryCast(tvi.TryFindResource("Theme_TextOnPrimaryBrush"), Brush)
                If brush IsNot Nothing Then
                    Return brush
                Else
                    Return Brushes.White ' safe fallback
                End If
            End If

            ' normal state – take the page’s own colour
            Dim pageInfo = TryCast(tvi.DataContext, PageInfo)
            Return If(pageInfo?.IconBrush, DependencyProperty.UnsetValue)
        End Function

        Public Function ConvertBack(value As Object, targetTypes As Type(), parameter As Object, culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
End Namespace
