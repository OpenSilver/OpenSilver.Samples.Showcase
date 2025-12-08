Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Globalization

Namespace Global.MaterialDesign_Styles_Kit
    Public Class AccentColorConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is SolidColorBrush Then
                Dim oldColor = CType(value, SolidColorBrush).Color
                Dim newR = If(oldColor.R > 40, CByte(oldColor.R - 40), CByte(0))
                Dim newG = If(oldColor.G > 40, CByte(oldColor.G - 40), CByte(0))
                Dim newB = If(oldColor.B > 40, CByte(oldColor.B - 40), CByte(0))
                Dim newColor = Color.FromArgb(oldColor.A, newR, newG, newB)
                Return New SolidColorBrush(newColor)
            End If
            Return value
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
