Imports System.Windows.Data
Imports System.Globalization

Namespace Global.OpenSilver.Samples.Showcase
    Public Class KilometersToMilesConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim kilometers As Integer
            If value IsNot Nothing AndAlso Integer.TryParse(value.ToString(), kilometers) Then
                Return CInt(kilometers * 0.62137119)
            Else
                Return ""
            End If
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim miles As Integer
            If value IsNot Nothing AndAlso Integer.TryParse(value.ToString(), miles) Then
                Return CInt(miles * 1.609344)
            Else
                Return ""
            End If
        End Function
    End Class
End Namespace
