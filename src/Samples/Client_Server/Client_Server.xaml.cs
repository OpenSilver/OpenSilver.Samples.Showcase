using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Client_Server : UserControl
    {
        public Client_Server()
        {
            this.InitializeComponent();

#if OPENSILVER
            //REST_WebClientDemo.Visibility = System.Windows.Visibility.Collapsed;
#endif
        }
    }
}
