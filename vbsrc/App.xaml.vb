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

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public NotInheritable Class App
        Inherits Application
        Public Sub New()
            Resources.Add("AccentColorConverter", New AccentColorConverter())
            Resources.Add("DoubleToCornerRadiusConverter", New DoubleToCornerRadiusConverter())
            Resources.Add("TextToPlaceholderTextVisibilityConverter", New TextToPlaceholderTextVisibilityConverter())
            Resources.Add("MaterialDesign_DefaultAccentColor", New SolidColorBrush(Color.FromArgb(255, 0, 105, 236)))

            Me.InitializeComponent()

            ' Enter construction logic here...

            Dim mainPage = New MainPage()
            Window.Current.Content = mainPage
        End Sub
    End Class
End Namespace
