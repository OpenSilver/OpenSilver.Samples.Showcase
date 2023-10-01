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
    Partial Public Class WebSockets_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonMakeWebSocketCall_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            '------------------------------------------------
            ' This will call the "Echo" service provided by https://javascript.info/websocket
            '------------------------------------------------

            Dim webSocket = New Extensions.WebSockets.WebSocket("wss://javascript.info/article/websocket/demo/hello")
            AddHandler webSocket.OnMessage, Sub(s, args) MessageBox.Show("The server returned the following message: " & args.Data)
            AddHandler webSocket.OnError, Sub(s, args) MessageBox.Show("ERROR: " & args.Data)
            AddHandler webSocket.OnClose, Sub(s, args) MessageBox.Show("WebSocket Closed")
            AddHandler webSocket.OnOpen, Sub(s, args)
                                             MessageBox.Show("WebSocket Opened")

                                             ' Send a message to the Echo service, which will respond with the same message:
                                             webSocket.Send("it works!")
                                         End Sub
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "WebSockets_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSockets_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "WebSockets_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSockets_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "WebSocket.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSocket.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "WebSockets_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Client_Server/WebSockets/WebSockets_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "WebSocket.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Client_Server/WebSockets/WebSocket.vb"
    }
})
        End Sub

    End Class
End Namespace
