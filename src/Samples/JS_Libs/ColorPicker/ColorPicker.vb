Imports CSHTML5.Native.Html.Controls
Imports System
Imports System.Globalization
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Media

Namespace OpenSilver.Samples.Showcase

    Public Class ColorPicker
        Inherits HtmlPresenter

        Private Shared _isJsLibLoaded As Boolean
        Private _domElement As Object

#Region "Color"
        Public Property Color() As Color
            Get
                Return DirectCast(GetValue(ColorProperty), Color)
            End Get
            Set(value As Color)
                SetValue(ColorProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ColorProperty As DependencyProperty = 
            DependencyProperty.Register(NameOf(Color), GetType(Color), GetType(ColorPicker), New PropertyMetadata(Colors.Black))
#End Region

        Public Sub New()
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Private Async Sub OnLoaded(sender As Object, e As RoutedEventArgs)
            RemoveHandler Loaded, AddressOf OnLoaded

            Await LoadJSLibrary()
            _domElement = Interop.GetDiv(Me)

            Html = "<div></div>"
            Interop.ExecuteJavaScriptVoidAsync($"
              const alwan = new Alwan($0.firstChild.firstChild, {{
                theme: 'dark',
                toggle: false,
                popover: false,
                color: {JsHexColor},
                margin: 0,
                format: 'rgb',
                inputs: {{hex: true, rgb: true,  hsl: true}},
                singleInput: true,
              }});
              alwan.on('color', (ev) => {{
                $1(ev.r, ev.g, ev.b, ev.a);
              }});
              alwan.on('change', (ev) => {{
                $1(ev.r, ev.g, ev.b, ev.a);
              }});
              $0.alwan = alwan;
            ", _domElement, DirectCast(AddressOf OnColorChangedInAlwan, Action(Of Byte, Byte, Byte, Single)))
        End Sub

        Private Sub OnColorChangedInAlwan(r As Byte, g As Byte, b As Byte, a As Single)
            Color = Color.FromArgb(CByte(Math.Round(a * Byte.MaxValue)), r, g, b)
        End Sub

        Private ReadOnly Property JsHexColor() As String
            Get
                Return $"'#{GetHex(Color.R)}{GetHex(Color.G)}{GetHex(Color.B)}{GetHex(Color.A)}'"
            End Get
        End Property

        Private Function GetHex(number As Byte) As String
            Return number.ToString("X2", CultureInfo.InvariantCulture)
        End Function

        Private Shared Async Function LoadJSLibrary() As Task
            If Not _isJsLibLoaded Then
                Await Interop.LoadCssFile("https://cdn.jsdelivr.net/npm/alwan/dist/css/alwan.min.css")
                Await Interop.LoadJavaScriptFile("https://cdn.jsdelivr.net/npm/alwan/dist/js/alwan.min.js")
                _isJsLibLoaded = True
            End If
        End Function
    End Class

End Namespace
