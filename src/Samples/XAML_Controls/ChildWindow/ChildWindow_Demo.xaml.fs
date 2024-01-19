namespace OpenSilver.Samples.Showcase

open PreviewOnWinRT
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

type ChildWindow_Demo() as this =
    inherit ChildWindow_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonTestChildWindow_Click(sender : obj, e : RoutedEventArgs) =
        let loginWnd = LoginWindow()
        loginWnd.Closed.Add(fun args -> this.loginWnd_Closed(loginWnd, args))
        loginWnd.Show()

    member private this.loginWnd_Closed(sender : obj, e : EventArgs) =
        let lw = sender :?> LoginWindow
        if lw.DialogResult.HasValue && lw.DialogResult.Value = true && lw.NameBox.Text <> String.Empty then
            this.TextBlockForTestingChildWindow.Text <- "Hello " + lw.NameBox.Text
        else
            this.TextBlockForTestingChildWindow.Text <- "Login cancelled."

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "ChildWindow_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/ChildWindow_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "ChildWindow_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/ChildWindow_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "ChildWindow_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/ChildWindow_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "ChildWindow_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/ChildWindow_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "LoginWindow.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/LoginWindow.xaml");
            ViewSourceButtonInfo(TabHeader = "LoginWindow.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/LoginWindow.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "LoginWindow.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/LoginWindow.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "LoginWindow.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/LoginWindow.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "DefaultChildWindowStyle.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/Styles/DefaultChildWindowStyle.xaml")
        ])
