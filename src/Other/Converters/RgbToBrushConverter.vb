Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media

Namespace OpenSilver.Samples.Showcase

    ''' <summary>
    ''' Converts three RGB values to a SolidColorBrush.
    ''' </summary>
    Public Class RgbToBrushConverter
        Implements IMultiValueConverter

        Public Function Convert(values As Object(), targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            If values.Length = 3 AndAlso
               TypeOf values(0) Is Double AndAlso
               TypeOf values(1) Is Double AndAlso
               TypeOf values(2) Is Double Then

                Dim r As Byte = CByte(values(0))
                Dim g As Byte = CByte(values(1))
                Dim b As Byte = CByte(values(2))

                Return New SolidColorBrush(Color.FromRgb(r, g, b))
            End If

            Return New SolidColorBrush(Colors.Black)
        End Function

        Public Function ConvertBack(value As Object, targetTypes As Type(), parameter As Object, culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

    End Class

End Namespace
