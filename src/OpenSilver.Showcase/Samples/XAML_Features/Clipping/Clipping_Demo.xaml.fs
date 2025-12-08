namespace OpenSilver.Showcase

open System.Windows
open System.Windows.Controls
open System.Windows.Media

type Clipping_Demo() as this =
    inherit Clipping_DemoXaml()

    do
        this.InitializeComponent()

        let clipCheckBox = this.FindName("clipCheckBox") :?> CheckBox
        clipCheckBox.Checked.Add(fun _ -> this.OnClipCheckBoxStateChanged())
        clipCheckBox.Unchecked.Add(fun _ -> this.OnClipCheckBoxStateChanged())

    member private this.OnClipCheckBoxStateChanged() =
        let clipCheckBox = this.FindName("clipCheckBox") :?> CheckBox
        let button = this.FindName("button") :?> Button
        if clipCheckBox.IsChecked.HasValue && clipCheckBox.IsChecked.Value then
            button.Clip <- EllipseGeometry(Center = Point(70.0, 30.0), RadiusX = 70.0, RadiusY = 30.0)
        else
            button.Clip <- null
