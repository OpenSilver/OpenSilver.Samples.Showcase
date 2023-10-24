Imports System.Collections.Generic
Imports System.Windows.Browser
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
#Else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#End If

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class HtmlPage_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonGetCurrentURL_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("The current URL is: " & HtmlPage.Document.DocumentUri.OriginalString)
        End Sub
    End Class
End Namespace
