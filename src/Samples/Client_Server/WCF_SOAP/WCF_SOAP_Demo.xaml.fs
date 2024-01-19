namespace OpenSilver.Samples.Showcase

#if OPENSILVER
// open ServiceReference1
#else
open OpenSilver.Samples.Showcase.ServiceReference1
#endif
open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Net
open System.Runtime.Serialization
open System.ServiceModel
open System.Text
open System.Threading.Tasks
open System.Windows
open System.Xml
open System.Xml.Serialization
#if SLMIGRATION
open System.Windows.Controls
#else
open Windows.UI
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Media
#endif

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

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "WCF_SOAP_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WCF_SOAP/WCF_SOAP_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "WCF_SOAP_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WCF_SOAP/WCF_SOAP_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "WCF_SOAP_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WCF_SOAP/WCF_SOAP_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "WCF_SOAP_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WCF_SOAP/WCF_SOAP_Demo.xaml.fs")
        ])
