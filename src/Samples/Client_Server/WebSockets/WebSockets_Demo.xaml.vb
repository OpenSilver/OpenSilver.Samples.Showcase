Imports System.Windows
Imports System.Windows.Controls

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
    End Class
End Namespace
