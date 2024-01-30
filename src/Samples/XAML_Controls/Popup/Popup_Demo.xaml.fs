namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type Popup_Demo() as this =
    inherit Popup_DemoXaml()
    
    do
        this.InitializeComponent()

    member private this.OpenPopupButton_Click(sender : obj, e : RoutedEventArgs) =
        this.MyPopup.IsOpen <- true

    member private this.PopupButtonClose_Click(sender : obj, e : RoutedEventArgs) =
        this.MyPopup.IsOpen <- false
