Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class Printing_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonPrint_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim invoiceToPrint = New Invoice()
            CSHTML5.Native.Html.Printing.PrintManager.Print(invoiceToPrint)
        End Sub
    End Class
End Namespace
