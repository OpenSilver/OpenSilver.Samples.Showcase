Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data

Namespace OpenSilver.Samples.Showcase
    Public Class StringEmptyToVisibilityConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is String AndAlso Not String.IsNullOrEmpty(DirectCast(value, String)) Then
                Return Visibility.Collapsed
            Else
                Return Visibility.Visible
            End If
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
