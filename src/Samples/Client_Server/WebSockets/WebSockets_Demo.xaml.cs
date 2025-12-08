using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("WebSockets", "network", "communication", "web")]
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
    }
}
