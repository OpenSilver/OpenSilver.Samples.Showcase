Imports System.Collections.Generic
Imports System.Text.RegularExpressions
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
    Partial Public Class Regex_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonReplaceDates_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.TextBlockOutputOfRegexReplaceDemo.Text = Regex.Replace(Me.TextBoxRegexReplaceDemo.Text, "(\d{2})/(\d{2})/(\d{2,4})", "$3-$2-$1")
        End Sub
    End Class
End Namespace
