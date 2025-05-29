Imports System.Windows
Imports System.Windows.Media

Namespace OpenSilver.Samples.Showcase
    Partial Public NotInheritable Class App
        Inherits Application

        Public Sub New()
            InitializeComponent()
            AddHandler Startup, AddressOf OnAppStartup
        End Sub

        Private Async Sub OnAppStartup(sender As Object, e As StartupEventArgs)
            Await FontFamily.LoadFontAsync("ms-appx:///OpenSilver.Samples.Showcase/Other/Inter_VariableFont_slnt_wght.ttf")

            Features.DOM.AssignClass = True
            Await Interop.LoadCssFile("ms-appx:///OpenSilver.Samples.Showcase/Other/CSS/app-styles.css")

            Dim mainPage As New MainPage()
            Window.Current.Content = mainPage
        End Sub
    End Class
End Namespace
