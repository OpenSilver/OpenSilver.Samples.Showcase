Imports System.Windows
Imports System.Windows.Data
Imports System.Globalization

Namespace Global.MaterialDesign_Styles_Kit
    Public Class TextToPlaceholderTextVisibilityConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is String Then
                If String.IsNullOrEmpty(CStr(value)) Then Return Visibility.Visible
                Return Visibility.Collapsed
            End If
            Throw New InvalidOperationException("The IValueConverter expects a value of type String.")
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
