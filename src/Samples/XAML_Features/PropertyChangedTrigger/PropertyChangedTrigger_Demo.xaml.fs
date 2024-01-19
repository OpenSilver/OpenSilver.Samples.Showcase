namespace OpenSilver.Samples.Showcase

open System.Collections.Generic

#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
#endif

type PropertyChangedTrigger_Demo() as this =
    inherit PropertyChangedTrigger_DemoXaml()

    do
        this.InitializeComponent(); 

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "PropertyChangedTrigger_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "PropertyChangedTrigger_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "PropertyChangedTrigger_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "PropertyChangedTrigger_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml.fs")
        ])

    member private this.Button_Click(sender : obj, e : RoutedEventArgs) =
        this.borderText.Text <- if this.borderText.Text = "Yellow" then "Orange" else "Yellow"
