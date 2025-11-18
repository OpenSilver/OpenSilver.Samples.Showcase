Imports System.Windows
Imports CSHTML5.Internal
Imports CSHTML5.Native.Html.Controls
Imports OpenSilver

Namespace TestNumericTextBox

    Public Class NumericTextBox
        Inherits HtmlPresenter

        Private _value As Integer = 0
        Private _domElement As Object

        Public Sub New()
            Html = "<input type='number' pattern='[0-9]*' style='width:100%;height:100%'>"
            AddHandler Loaded, AddressOf NumericTextBox_Loaded
        End Sub

        Public Property Value As Integer
            Get
                If _domElement IsNot Nothing Then
                    Dim id As String = TryCast(_domElement, INTERNAL_HtmlDomElementReference).UniqueIdentifier
                    Dim valueString As String = Interop.ExecuteJavaScriptGetResult(Of String)($"{id}.firstChild.firstChild.value")
                    Dim valueInt As Integer
                    If Integer.TryParse(valueString, valueInt) Then
                        _value = valueInt
                    End If
                End If
                Return _value
            End Get
            Set(ByVal value As Integer)
                _value = value
                If _domElement IsNot Nothing Then
                    UpdateValue()
                End If
            End Set
        End Property

        Private Sub NumericTextBox_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            _domElement = Interop.GetDiv(Me)
            UpdateValue()
        End Sub

        Private Sub UpdateValue()
            Interop.ExecuteJavaScriptVoidAsync("$0.firstChild.firstChild.value = $1", _domElement, _value)
        End Sub
    End Class

End Namespace
