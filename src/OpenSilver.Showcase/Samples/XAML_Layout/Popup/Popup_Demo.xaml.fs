namespace OpenSilver.Showcase

open System.Windows
open OpenSilver.Showcase.Search

[<SearchKeywords("popup", "modal", "window", "overlay", "UI")>]
type Popup_Demo() as this =
    inherit Popup_DemoXaml()
    
    do
        this.InitializeComponent()

    member private this.OpenPopupButton_Click(sender : obj, e : RoutedEventArgs) =
        this.MyPopup.IsOpen <- true

    member private this.PopupButtonClose_Click(sender : obj, e : RoutedEventArgs) =
        this.MyPopup.IsOpen <- false
