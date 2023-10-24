Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class Window_SizeChanged_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            AddHandler Loaded, AddressOf Window_SizeChanged_Demo_Loaded
            AddHandler Unloaded, AddressOf Window_SizeChanged_Demo_Unloaded

            SetWindowSize()
        End Sub

#Region "Size Changed Events"

        'When the window is loaded, we add the event Current_SizeChanged
        Private Sub Window_SizeChanged_Demo_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AddHandler Window.Current.SizeChanged, AddressOf Current_SizeChanged
        End Sub

        'When the window is unloaded, we remove the event Current_SizeChanged
        Private Sub Window_SizeChanged_Demo_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RemoveHandler Window.Current.SizeChanged, AddressOf Current_SizeChanged
        End Sub
#End Region

        Private Sub Current_SizeChanged(ByVal sender As Object, ByVal e As WindowSizeChangedEventArgs)

            Me.TextBlockValueX.Text = (If(Double.IsNaN(e.Size.Width), "NaN", e.Size.Width.ToString()))
            Me.TextBlockValueY.Text = (If(Double.IsNaN(e.Size.Height), "NaN", e.Size.Height.ToString()))
        End Sub

        Private Sub SetWindowSize()
            Me.TextBlockValueX.Text = Window.Current.Bounds.Width.ToString()
            Me.TextBlockValueY.Text = Window.Current.Bounds.Height.ToString()
        End Sub
    End Class
End Namespace
