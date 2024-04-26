Imports CSHTML5.Native.Html.Controls
Imports System.Windows.Controls

Namespace Global.TestNumericTextBox
    Public Class NumericTextBox
        Inherits HtmlPresenter
        Private _value As Integer = 0

        Public Sub New()
            Html = "<input type=""number"" pattern=""[0-9]*"" style=""width:100%;height:100%"">"

            ScrollMode = ScrollMode.Disabled

            AddHandler Loaded, AddressOf NumericTextBox_Loaded
        End Sub

        Public Property Value As Integer
            Get
                If DomElement IsNot Nothing Then 'Note: the DOM element is null if the control has not been added to the visual tree yet.
                    Dim valueInt As Integer
                    Dim valueString As String = OpenSilver.Interop.ExecuteJavaScript("$0.value", DomElement).ToString()
                    If Integer.TryParse(valueString, valueInt) Then
                        _value = valueInt
                    End If
                End If
                Return _value
            End Get
            Set(ByVal value As Integer)
                _value = value

                If DomElement IsNot Nothing Then OpenSilver.Interop.ExecuteJavaScript("$0.value = $1", DomElement, _value) 'Note: the DOM element is null if the control has not been added to the visual tree yet.
            End Set
        End Property

        Private Sub NumericTextBox_Loaded(ByVal sender As Object, ByVal e As Windows.RoutedEventArgs)
            ' Here, the control has been added to the visual tree, so the DOM element exists. We set the initial value:
            OpenSilver.Interop.ExecuteJavaScript("$0.value = $1", DomElement, _value)
        End Sub
    End Class
End Namespace
