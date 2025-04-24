Imports System.IO
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports CSHTML5.Native.Html.Controls

Namespace OpenSilver.Samples.Showcase
    Public Class SvgImage
        Inherits HtmlPresenter

        Public Property Source As String
            Get
                Return CType(GetValue(SourceProperty), String)
            End Get
            Set(value As String)
                SetValue(SourceProperty, value)
            End Set
        End Property

        Public Shared ReadOnly SourceProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(Source), GetType(String), GetType(SvgImage), New PropertyMetadata(Nothing, AddressOf OnSourceChanged))

        Private Shared Sub OnSourceChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim control = CType(d, SvgImage)
            Dim source = CType(e.NewValue, String)

            If String.IsNullOrEmpty(source) Then
                control.Content = ""
            Else
                Dim isAbsolutePath = source.Contains(";component/") OrElse
                                     source.StartsWith("http") OrElse
                                     source.StartsWith("pack:/") OrElse
                                     source.StartsWith("ms-appx:/")

                If Not isAbsolutePath Then
                    Dim assembly = control.GetType().Assembly
                    source = $"/{assembly.GetName().Name};component/{source}"
                End If

                Dim stream = Application.GetResourceStream(New Uri(source, UriKind.Relative)).Result?.Stream
                If stream Is Nothing Then
                    control.Content = $"<p style='color:red'>Icon '{source}' is not found</p>"
                Else
                    Using reader As New StreamReader(stream)
                        control.Content = reader.ReadToEnd()
                    End Using
                End If
            End If
        End Sub

        Public Property Content As String
            Get
                Return CType(GetValue(ContentProperty), String)
            End Get
            Set(value As String)
                SetValue(ContentProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ContentProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(Content), GetType(String), GetType(SvgImage),
                New PropertyMetadata(String.Empty) With {
                    .MethodToUpdateDom = Sub(d, newValue)
                                             Dim control = CType(d, SvgImage)
                                             control.Html = CType(newValue, String)
                                             control.SetAutoSize()
                                             control.UpdateFillColor()
                                             control.UpdateStrokeColor()
                                         End Sub
                })

        Public Property FillElementSelector As String = "path"
        Public Property ForceSetFill As Boolean = False

        Public Property FillColor As Nullable(Of Color)
            Get
                Return CType(GetValue(FillColorProperty), Nullable(Of Color))
            End Get
            Set(value As Nullable(Of Color))
                SetValue(FillColorProperty, value)
            End Set
        End Property

        Public Shared ReadOnly FillColorProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(FillColor), GetType(Nullable(Of Color)), GetType(SvgImage),
                New PropertyMetadata(Nothing) With {.MethodToUpdateDom = Sub(d, e) CType(d, SvgImage).UpdateFillColor()})

        Private Sub UpdateFillColor()
            UpdateColor(FillColor, FillElementSelector, "fill", ForceSetFill)
        End Sub

        Public Property StrokeElementSelector As String = "path"
        Public Property ForceSetStroke As Boolean = False

        Public Property StrokeColor As Nullable(Of Color)
            Get
                Return CType(GetValue(StrokeColorProperty), Nullable(Of Color))
            End Get
            Set(value As Nullable(Of Color))
                SetValue(StrokeColorProperty, value)
            End Set
        End Property

        Public Shared ReadOnly StrokeColorProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(StrokeColor), GetType(Nullable(Of Color)), GetType(SvgImage),
                New PropertyMetadata(Nothing) With {.MethodToUpdateDom = Sub(d, e) CType(d, SvgImage).UpdateStrokeColor()})

        Private Sub UpdateStrokeColor()
            UpdateColor(StrokeColor, StrokeElementSelector, "stroke", ForceSetStroke)
        End Sub

        Private Sub UpdateColor(color As Nullable(Of Color), elementSelector As String, attributeName As String, forceUpdate As Boolean)
            If color Is Nothing Then Return

            Dim hexARGB = color.ToString() ' #AARRGGBB
            Dim jsColor = $"#{hexARGB.Substring(3, 6)}{hexARGB.Substring(1, 2)}"

            Interop.ExecuteJavaScriptAsync($"
$0.firstChild.shadowRoot
  .querySelectorAll('{elementSelector}')
  .forEach(el => {{
    if ({forceUpdate.ToString().ToLower()} || (el.hasAttribute('{attributeName}') && el.getAttribute('{attributeName}') != 'none')) {{
      el.setAttribute('{attributeName}', '{jsColor}');
    }}
  }});
", Interop.GetDiv(Me))
        End Sub

        Public Sub New()
            ScrollMode = Windows.Controls.ScrollMode.Disabled
            Dim shadowDom = Me.GetType().GetProperty(NameOf(UseShadowDom))
            shadowDom?.SetValue(Me, True)
        End Sub

        Private Sub SetAutoSize()
            Interop.ExecuteJavaScriptVoidAsync($"
var svg = $0.firstChild.shadowRoot.querySelector('svg');
if (svg) {{
  svg.setAttribute('width', '100%');
  svg.setAttribute('height', '100%');
}}
", Interop.GetDiv(Me))
        End Sub

        Protected Overrides Sub OnMouseWheel(e As MouseWheelEventArgs)
            MyBase.OnMouseWheel(e)
            e.Handled = False
        End Sub
    End Class
End Namespace
