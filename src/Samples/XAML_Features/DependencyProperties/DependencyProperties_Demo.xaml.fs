namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type DependencyProperties_Demo() as this =
    inherit DependencyProperties_DemoXaml()

    do
        this.InitializeComponent()

    static member DependencyProperty_Changed (d : DependencyObject) (newValue : obj) =
        MessageBox.Show("The DependencyProperty value has changed!") |> ignore

    static member mySampleDependencyPropertyProperty =
        DependencyProperty.Register(
            "MySampleDependencyProperty",
            typeof<int>,
            typeof<DependencyProperties_Demo>,
            new PropertyMetadata(defaultValue = 0, MethodToUpdateDom = DependencyProperties_Demo.DependencyProperty_Changed))

    member this.MySampleDependencyProperty
        with get() = (this.GetValue(DependencyProperties_Demo.mySampleDependencyPropertyProperty) :?> int)
        and set(value) = this.SetValue(DependencyProperties_Demo.mySampleDependencyPropertyProperty, value)

    member this.Decrementation_Click(sender : obj, e : RoutedEventArgs) =
        this.MySampleDependencyProperty <- this.MySampleDependencyProperty - 1

    member this.Incrementation_Click(sender : obj, e : RoutedEventArgs) =
        this.MySampleDependencyProperty <- this.MySampleDependencyProperty + 1
