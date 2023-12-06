Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    Partial Public Class InitParams_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonShowInitParams_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim parameters = Application.Current.Host.InitParams
            Dim initParamsString As String = "I found this in init param:"

            For Each param In parameters
                initParamsString += $"{Environment.NewLine}key: {param.Key}, value: {param.Value}"
            Next

            MessageBox.Show(initParamsString)
        End Sub
    End Class
End Namespace
