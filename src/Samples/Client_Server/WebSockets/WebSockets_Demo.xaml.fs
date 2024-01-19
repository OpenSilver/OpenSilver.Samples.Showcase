namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.Foundation
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
#endif

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

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "WebSockets_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSockets_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "WebSockets_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSockets_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "WebSocket.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSocket.cs");
            ViewSourceButtonInfo(TabHeader = "WebSockets_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSockets_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "WebSocket.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSocket.vb");
            ViewSourceButtonInfo(TabHeader = "WebSockets_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSockets_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "WebSocket.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSocket.fs")
        ])
