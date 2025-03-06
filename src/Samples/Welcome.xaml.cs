using Microsoft.Maui.Devices;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Welcome : UserControl
    {
        public Welcome()
        {
            this.InitializeComponent();
#if OPENSILVER
            IntroductionTextBlock.Text = "This app was written in standard C# and XAML, and compiled to WebAssembly using OpenSilver.";
#endif
        }
    }
}
