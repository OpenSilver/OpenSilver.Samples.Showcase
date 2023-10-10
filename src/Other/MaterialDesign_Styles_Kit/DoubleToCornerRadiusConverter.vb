Imports System
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Data
Imports System.Globalization
#Else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#End If

Namespace Global.MaterialDesign_Styles_Kit
    Public Class DoubleToCornerRadiusConverter
        Implements IValueConverter
#If SLMIGRATION
#Else
        public object Convert(object value, Type targetType, object parameter, string language)
#End If
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Double Then
                Return New CornerRadius(value)
            End If
            Return value
        End Function

#If SLMIGRATION
#Else
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#End If
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
