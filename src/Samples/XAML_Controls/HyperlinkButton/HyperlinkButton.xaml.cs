using System;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class HyperlinkButton_Demo : UserControl
    {
        public HyperlinkButton_Demo()
        {
            this.InitializeComponent();

#if OPENSILVER
            HyperlinkButtonDemo.NavigateUri = new Uri("http://www.opensilver.net"); 
    #endif

        }
    }
}
