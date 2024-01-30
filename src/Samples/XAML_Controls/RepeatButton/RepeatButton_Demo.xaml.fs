namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media

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
