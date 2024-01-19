namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Controls.Primitives
open System.Windows.Data
open System.Windows.Input
open System.Windows.Media
open System.Windows.Navigation
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

type DateAndTimePicker_Demo() as this =
    inherit DateAndTimePicker_DemoXaml()

    do
        this.InitializeComponent()

    member private this.RegisterEvent_Click(sender : obj, e : RoutedEventArgs) =
        if this.TimePicker.Value.HasValue && this.DatePicker.SelectedDate.HasValue then
            MessageBox.Show($"Your event is confirmed at {this.TimePicker.Value.Value.ToShortTimeString()} on {this.DatePicker.SelectedDate.Value.ToShortDateString()}") |> ignore
        else
            MessageBox.Show("Please enter a Date and a Time") |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "DateAndTimePicker_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DateAndTimePicker/DateAndTimePicker_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "DateAndTimePickerd_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DateAndTimePicker/DateAndTimePicker_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "DateAndTimePickerd_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DateAndTimePicker/DateAndTimePicker_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "DateAndTimePickerd_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DateAndTimePicker/DateAndTimePicker_Demo.xaml.fs")
        ])
