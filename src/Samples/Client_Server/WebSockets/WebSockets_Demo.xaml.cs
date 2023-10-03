using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
{
    public partial class WebSockets_Demo : UserControl
    {
        public WebSockets_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonMakeWebSocketCall_Click(object sender, RoutedEventArgs e)
        {
            //------------------------------------------------
            // This will call the "Echo" service provided by https://javascript.info/websocket
            //------------------------------------------------

            var webSocket = new OpenSilver.Extensions.WebSockets.WebSocket("wss://javascript.info/article/websocket/demo/hello");
            webSocket.OnMessage += (s, args) => MessageBox.Show("The server returned the following message: " + args.Data);
            webSocket.OnError += (s, args) => MessageBox.Show("ERROR: " + args.Data);
            webSocket.OnClose += (s, args) => MessageBox.Show("WebSocket Closed");
            webSocket.OnOpen += (s, args) =>
            {
                MessageBox.Show("WebSocket Opened");

                // Send a message to the Echo service, which will respond with the same message:
                webSocket.Send("it works!");
            };
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "WebSockets_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSockets_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "WebSockets_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSockets_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "WebSocket.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSocket.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "WebSockets_Demo.xaml.vb",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSockets_Demo.xaml.vb"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "WebSocket.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WebSockets/WebSocket.vb"
                }
            });
        }

    }
}
