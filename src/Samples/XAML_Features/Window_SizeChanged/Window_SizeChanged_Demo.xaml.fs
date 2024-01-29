namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls

type Window_SizeChanged_Demo() as this =
    inherit Window_SizeChanged_DemoXaml()

    let sizeChangedHandler =
        new WindowSizeChangedEventHandler(fun sender args -> this.Current_SizeChanged(sender, args) )

    do
        this.InitializeComponent()

        // Add Loaded and Unloaded event handlers
        this.Loaded.Add(fun args -> this.Window_SizeChanged_Demo_Loaded(this, args))
        this.Unloaded.Add(fun args -> this.Window_SizeChanged_Demo_Unloaded(this, args))

        // Set initial window size
        this.SetWindowSize()

    //When the window is loaded, we add the event Current_SizeChanged
    member private this.Window_SizeChanged_Demo_Loaded(sender : obj, e : RoutedEventArgs) =
        Window.Current.SizeChanged.AddHandler(sizeChangedHandler)

    //When the window is unloaded, we remove the event Current_SizeChanged
    member private this.Window_SizeChanged_Demo_Unloaded(sender : obj, e : RoutedEventArgs) =
        Window.Current.SizeChanged.RemoveHandler(sizeChangedHandler)

    member private this.Current_SizeChanged(sender : obj, e : WindowSizeChangedEventArgs) =
        this.TextBlockValueX.Text <- if Double.IsNaN(e.Size.Width) then "NaN" else e.Size.Width.ToString("G")
        this.TextBlockValueY.Text <- if Double.IsNaN(e.Size.Height) then "NaN" else e.Size.Height.ToString("G")

    // Set initial window size
    member private this.SetWindowSize() =
        this.TextBlockValueX.Text <- Window.Current.Bounds.Width.ToString("G")
        this.TextBlockValueY.Text <- Window.Current.Bounds.Height.ToString("G")
