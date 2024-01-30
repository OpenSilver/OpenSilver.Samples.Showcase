namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type WebSockets_Demo() as this =
    inherit WebSockets_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.ButtonMakeWebSocketCall_Click(sender : obj, e : RoutedEventArgs) =
        //------------------------------------------------
        // This will call the "Echo" service provided by https://javascript.info/websocket
        //------------------------------------------------

        let webSocket = OpenSilver.Extensions.WebSockets.WebSocket("wss://javascript.info/article/websocket/demo/hello")
        webSocket.OnMessage.Add(fun args -> MessageBox.Show("The server returned the following message: " + args.Data) |> ignore)
        webSocket.OnError.Add(fun args -> MessageBox.Show("ERROR: " + args.Data) |> ignore)
        webSocket.OnClose.Add(fun args -> MessageBox.Show("WebSocket Closed") |> ignore)
        webSocket.OnOpen.Add(fun args ->
            MessageBox.Show("WebSocket Opened") |> ignore

            // Send a message to the Echo service, which will respond with the same message:
            webSocket.Send("it works!")
        )
