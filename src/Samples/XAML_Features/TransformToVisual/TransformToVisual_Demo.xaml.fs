namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search
open System.Windows

[<SearchKeywords("position", "relative")>]
type TransformToVisual_Demo() as this =
    inherit TransformToVisual_DemoXaml()

    do
        this.InitializeComponent()
        this.Loaded.AddHandler(RoutedEventHandler(fun s e -> this.OnLoaded(s, e)))
        Application.Current.MainWindow.SizeChanged.Add(fun _ -> this.CalculatePosition())

    member private this.OnLoaded(sender: obj, e: RoutedEventArgs) =
        match this.Parent with
        | :? FrameworkElement as parentElement ->
            parentElement.LayoutUpdated.Add(fun _ -> this.CalculatePosition())
        | _ -> ()

    member private this.CalculatePosition() =
        let transform = this.TransformToVisual(Application.Current.MainWindow)
        let topLeft = transform.Transform(Point())
        this.resultTextBlock.Text <- sprintf "X: %.1f  Y: %.1f" topLeft.X topLeft.Y
