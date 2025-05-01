Imports System.Globalization
Imports System.Windows
Imports System.Windows.Media
Imports CSHTML5.Native.Html.Controls

Namespace OpenSilver.Samples.Showcase

    Public Class HtmlColorPicker
        Inherits HtmlPresenter

        Private _domElement As Object
        Private _jsHexColor As String

        Public Sub New()
            Html = "<input type='color'>"
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Public Property Color As Color
            Get
                Return CType(GetValue(ColorProperty), Color)
            End Get
            Set(value As Color)
                SetValue(ColorProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ColorProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(Color), GetType(Color), GetType(HtmlColorPicker),
                                        New PropertyMetadata(Colors.Black, AddressOf OnColorChanged))

        Private Shared Sub OnColorChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim picker = CType(d, HtmlColorPicker)
            picker.UpdateValue()
        End Sub

        Private Sub OnLoaded(sender As Object, e As RoutedEventArgs)
            RemoveHandler Loaded, AddressOf OnLoaded

            _domElement = Interop.GetDiv(Me)
            _jsHexColor = JsHexColor

            Interop.ExecuteJavaScriptVoidAsync("
                const picker = $0.firstChild.firstChild;
                picker.value = $1;
                picker.addEventListener('input', e => $2(e.target.value));
            ", _domElement, _jsHexColor, CType(AddressOf OnJsColorChanged, Action(Of String)))
        End Sub

        Private Sub OnJsColorChanged(jsHexColor As String)
            If _jsHexColor <> jsHexColor AndAlso jsHexColor IsNot Nothing AndAlso jsHexColor.StartsWith("#"c) AndAlso jsHexColor.Length = 7 Then
                _jsHexColor = jsHexColor
                Dim r As Byte = Convert.ToByte(jsHexColor.Substring(1, 2), 16)
                Dim g As Byte = Convert.ToByte(jsHexColor.Substring(3, 2), 16)
                Dim b As Byte = Convert.ToByte(jsHexColor.Substring(5, 2), 16)
                Color = Color.FromRgb(r, g, b)
            End If
        End Sub

        Private Sub UpdateValue()
            If _domElement Is Nothing Then Return

            _jsHexColor = JsHexColor
            Interop.ExecuteJavaScriptVoidAsync("$0.firstChild.firstChild.value = $1", _domElement, _jsHexColor)
        End Sub

        Private ReadOnly Property JsHexColor As String
            Get
                Return $"#{GetHex(Color.R)}{GetHex(Color.G)}{GetHex(Color.B)}".ToLower()
            End Get
        End Property

        Private Function GetHex(number As Byte) As String
            Return number.ToString("X2", CultureInfo.InvariantCulture)
        End Function

    End Class

End Namespace
