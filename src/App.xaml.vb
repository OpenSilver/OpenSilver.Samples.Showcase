Imports System.Windows

Namespace OpenSilver.Samples.Showcase
    Partial Public NotInheritable Class App
        Inherits Application
        Public Sub New()

            Me.InitializeComponent()

            ' Enter construction logic here...

            Dim mainPage = New MainPage()
            Window.Current.Content = mainPage
        End Sub
    End Class
End Namespace
