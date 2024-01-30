namespace OpenSilver.Samples.Showcase

open System
open System.Windows.Controls

type HyperlinkButton_Demo() as this =
    inherit HyperlinkButton_DemoXaml()
    
    do
        this.InitializeComponent()

#if OPENSILVER
        this.HyperlinkButtonDemo.NavigateUri <- Uri("http://www.opensilver.net")
#endif
