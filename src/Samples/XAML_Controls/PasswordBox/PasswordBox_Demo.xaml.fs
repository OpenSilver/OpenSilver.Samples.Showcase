namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.Foundation
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
#endif

type PasswordBox_Demo() as this =
    inherit PasswordBox_DemoXaml()

    do
        this.InitializeComponent()

    member private this.Button_Click(sender : obj, e : RoutedEventArgs) =
        this.DisplayPasswordIfNotEmpty()

    member private this.DisplayPasswordIfNotEmpty() =
        if this.PasswordBox.Password.Length <> 0 then
            MessageBox.Show($"The password typed is\n\"{this.PasswordBox.Password}\"") |> ignore
        else
            MessageBox.Show("Please enter a password") |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "PasswordBox_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/PasswordBox/PasswordBox_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "PasswordBox_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/PasswordBox/PasswordBox_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "PasswordBox_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/PasswordBox/PasswordBox_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "PasswordBox_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/PasswordBox/PasswordBox_Demo.xaml.fs")
        ])
