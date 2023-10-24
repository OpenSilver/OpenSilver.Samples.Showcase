Imports System
Imports System.Collections.Generic
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
    Partial Public Class HyperlinkButton_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

#If OPENSILVER Then
            Me.HyperlinkButtonDemo.NavigateUri = New Uri("http://www.opensilver.net")
#End If

        End Sub
    End Class
End Namespace
