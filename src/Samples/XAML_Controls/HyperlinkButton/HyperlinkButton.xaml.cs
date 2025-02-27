using OpenSilver.Samples.Showcase.Search;
using System;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("hyperlink", "text", "navigation", "control", "button", "web")]
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
