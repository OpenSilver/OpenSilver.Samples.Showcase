namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls

type WebView_Demo() as this =
    inherit WebView_DemoXaml()
    
    do
        this.InitializeComponent()

    member private this.ButtonNavigate_Click(sender : obj, e : RoutedEventArgs) =
        this.WebView1.Navigate(new Uri(this.TextBoxWithURL.Text))
