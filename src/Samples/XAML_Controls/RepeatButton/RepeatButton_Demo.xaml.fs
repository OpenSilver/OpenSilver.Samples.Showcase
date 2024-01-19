namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Media
#else
open Windows.Foundation
open Windows.UI
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
#endif

type RepeatButton_Demo() as this =
    inherit RepeatButton_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.ButtonTranslate_Click(sender : obj, e : RoutedEventArgs) =
        if this.TestTransformBorder.RenderTransform = null || not (this.TestTransformBorder.RenderTransform :? TranslateTransform) then
            let translateTransform = TranslateTransform()
            this.TestTransformBorder.RenderTransform <- translateTransform

        let translateTransform = this.TestTransformBorder.RenderTransform :?> TranslateTransform
        translateTransform.X <- translateTransform.X + 10.0
        translateTransform.Y <- translateTransform.Y + 10.0

    member this.ButtonRotate_Click(sender : obj, e : RoutedEventArgs) =
        if this.TestTransformBorder.RenderTransform = null || not (this.TestTransformBorder.RenderTransform :? RotateTransform) then
            let rotateTransform = RotateTransform()
            this.TestTransformBorder.RenderTransform <- rotateTransform

        let rotateTransform = this.TestTransformBorder.RenderTransform :?> RotateTransform
        rotateTransform.Angle <- rotateTransform.Angle + 10.0

    member this.TransformButton_Click(sender : obj, e : RoutedEventArgs) =
        let r = Random()
        let brush = SolidColorBrush()

        brush.Color <- Color.FromArgb(byte (r.Next(255)), byte (r.Next(255)), byte (r.Next(255)), byte (r.Next(255)))
        this.TestTransformBorder.Background <- brush

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "RepeatButton_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/RepeatButton/RepeatButton_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "RepeatButton_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/RepeatButton/RepeatButton_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "RepeatButton_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/RepeatButton/RepeatButton_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "RepeatButton_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/RepeatButton/RepeatButton_Demo.xaml.fs")
        ])
