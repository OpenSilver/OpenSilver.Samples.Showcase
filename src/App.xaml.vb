Imports MaterialDesign_Styles_Kit
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Media
#Else
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#End If

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
