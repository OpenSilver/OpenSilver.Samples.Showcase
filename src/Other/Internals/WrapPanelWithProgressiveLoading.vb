

#If SLMIGRATION
Imports System.Windows.Controls
#Else
using Windows.UI.Xaml.Controls;
#End If

Namespace Global.OpenSilver.Samples.Showcase
    Public Class WrapPanelWithProgressiveLoading
        Inherits WrapPanel
        Public Sub New()
            ProgressiveRenderingChunkSize = 3
        End Sub
    End Class
End Namespace
