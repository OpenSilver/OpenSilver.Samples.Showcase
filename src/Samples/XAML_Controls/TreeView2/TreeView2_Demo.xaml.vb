Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    Partial Public Class TreeView2_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
            Me.DataContext = League.Leagues
        End Sub
    End Class
End Namespace
