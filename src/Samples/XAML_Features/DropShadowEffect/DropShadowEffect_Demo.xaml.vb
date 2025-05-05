Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("graphics", "shadow", "effect", "blur", "UI")>
    Partial Public Class DropShadowEffect_Demo
        Inherits UserControl
        Public Sub New()
            InitializeComponent()

            ColorPicker.SetBinding(
            HtmlColorPicker.ColorProperty,
            New Windows.Data.Binding("Color") With {
                .ElementName = NameOf(RectangleShadow),
                .Mode = Windows.Data.BindingMode.TwoWay
            })
        End Sub
    End Class
End Namespace
