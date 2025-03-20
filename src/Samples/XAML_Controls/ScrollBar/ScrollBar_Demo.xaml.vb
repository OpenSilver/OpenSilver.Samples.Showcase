Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("scroll", "bar", "navigation", "UI", "container")>
    Partial Public Class ScrollBar_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            Me.TextDisplay.Text = Me.Scrollbar.Value.ToString("0.000")
        End Sub

        Private Sub ScrollBar_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)
            Me.TextDisplay.Text = e.NewValue.ToString("0.000")
        End Sub

    End Class
End Namespace
