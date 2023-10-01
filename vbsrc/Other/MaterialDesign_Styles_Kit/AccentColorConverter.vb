Imports System

#If SLMIGRATION
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Globalization
#Else
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
#End If

Namespace Global.MaterialDesign_Styles_Kit
    Public Class AccentColorConverter
        Implements IValueConverter
#If SLMIGRATION Then
#Else
        public object Convert(object value, Type targetType, object parameter, string language)
#End If
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

#If SLMIGRATION Then
#Else
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#End If
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
