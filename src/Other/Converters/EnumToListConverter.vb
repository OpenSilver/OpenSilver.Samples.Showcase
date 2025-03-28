Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data

Namespace OpenSilver.Samples.Showcase
    Public Class EnumToListConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf parameter Is Type Then
                Dim type = DirectCast(parameter, Type)
                Return GetValues(type)
            ElseIf TypeOf parameter Is DependencyProperty Then
                Dim dp = DirectCast(parameter, DependencyProperty)
                Return GetValues(dp.PropertyType)
            End If

            Return Nothing
        End Function

        Private Shared Function GetValues(type As Type) As Object
            Dim underlyingType = Nullable.GetUnderlyingType(type)
            Dim enumType = If(underlyingType, type)

            If enumType.IsEnum Then
                Dim values = [Enum].GetValues(enumType)
                If underlyingType IsNot Nothing Then
                    Return (New Object() {Nothing}).Concat(values.Cast(Of Object)()).ToArray()
                Else
                    Return values
                End If
            End If

            Return Nothing
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
