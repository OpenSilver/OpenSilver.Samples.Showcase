Imports System
Imports System.Collections.Generic
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
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
    Partial Public Class Keyboard_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

#If SLMIGRATION Then
        Private Sub TextBoxInput_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
#Else
        private void TextBoxInput_KeyDown(object sender, KeyRoutedEventArgs e) 
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
#End If
            If e.Key = Key.Enter Then MessageBox.Show("You pressed Enter!" & Environment.NewLine & Environment.NewLine & "This is the text that you entered: " & Me.TextBoxInput.Text)
        End Sub
    End Class
End Namespace
