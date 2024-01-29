namespace OpenSilver.Samples.Showcase

// open ServiceReference1
open System.ServiceModel
open System.Windows
open System.Windows.Controls

type WCF_SOAP_Demo() as this = 
    inherit WCF_SOAP_DemoXaml()

    do
        this.InitializeComponent()
        this.Visibility <- Visibility.Collapsed

    member private this.RefreshSoapToDos () =
        ()

    member private this.ButtonRefreshSoapToDos_Click(sender : obj, e : RoutedEventArgs) =
        ()

    member private this.ButtonAddSoapToDo_Click(sender : obj, e : RoutedEventArgs) =
        ()

    member private this.ButtonDeleteSoapToDo_Click(sender : obj, e : RoutedEventArgs) =
        ()
