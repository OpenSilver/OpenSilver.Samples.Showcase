Imports System.Windows
Imports System.Windows.Data
Imports System.Globalization

Namespace Global.MaterialDesign_Styles_Kit
    Public Class DoubleToCornerRadiusConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Double Then
                Return New CornerRadius(value)
            End If
            Return value
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
