Imports OpenSilver.Samples.Showcase.Search
Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("performance", "virtualization", "lazy loading", "memory optimization", "UI")>
    Partial Public Class Virtualization_Demo
        Inherits UserControl

        Public colors As List(Of Tuple(Of String, String)) = GetColorList()

        Public Sub New()
            Me.InitializeComponent()
            Me.DataContext = colors
        End Sub

        Private Shared Function GetColorList() As List(Of Tuple(Of String, String))
            Dim colorList As List(Of Tuple(Of String, String)) = New List(Of Tuple(Of String, String))()

            For Each [property] As PropertyInfo In GetType(Colors).GetProperties()

                If [property].PropertyType = GetType(Color) Then
                    Dim color As Color = CType([property].GetValue(Nothing, Nothing), Color)
                    Dim colorName As String = [property].Name
                    Dim colorCode As String = "#" & color.ToString().Substring(3) 'Convert to hex format
                    colorList.Add(Tuple.Create(colorName, colorCode))
                End If
            Next

            Return colorList
        End Function
    End Class
End Namespace
