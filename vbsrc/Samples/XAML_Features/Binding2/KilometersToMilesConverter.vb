Imports System

#If SLMIGRATION
Imports System.Windows.Data
Imports System.Globalization
#Else
using Windows.UI.Xaml.Data;
#End If

Namespace Global.OpenSilver.Samples.Showcase
    Public Class KilometersToMilesConverter
        Implements IValueConverter
#If SLMIGRATION
#Else
        public object Convert(object value, Type targetType, object parameter, string language)
#End If
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim kilometers As Integer
            If value IsNot Nothing AndAlso Integer.TryParse(value.ToString(), kilometers) Then
                Return CInt(kilometers * 0.62137119)
            Else
                Return ""
            End If
        End Function

#If SLMIGRATION
#Else
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#End If
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim miles As Integer
            If value IsNot Nothing AndAlso Integer.TryParse(value.ToString(), miles) Then
                Return CInt(miles * 1.6093440)
            Else
                Return ""
            End If
        End Function
    End Class
End Namespace
