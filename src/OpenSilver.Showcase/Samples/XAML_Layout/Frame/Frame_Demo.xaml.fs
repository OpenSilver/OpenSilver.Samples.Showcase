namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search
open System.Windows
open System.Windows.Controls
open System.Windows.Media

[<SearchKeywords("navigation", "navigationservice", "page", "content", "view", "container", "query", "urimapping", "urimapper", "url", "back", "forward")>]
type Frame_Demo() as this =
    inherit Frame_DemoXaml()

    let mutable page : Page = null

    do
        this.InitializeComponent()
        this.Loaded.AddHandler(new RoutedEventHandler(fun sender args -> this.OnLoaded(sender, args)))

    member private this.OnLoaded(sender: obj, e: RoutedEventArgs) =
        page <- Frame_Demo.GetParentPage(this)
        this.UpdateButtonsState()

    member private this.UpdateButtonsState() =
        this.ButtonBackwards.IsEnabled <- page.NavigationService.CanGoBack
        this.ButtonForward.IsEnabled <- page.NavigationService.CanGoForward

    member private this.ButtonBackwards_Click(sender: obj, e: RoutedEventArgs) =
        if page.NavigationService.CanGoBack then
            page.NavigationService.GoBack()

    member private this.ButtonForward_Click(sender: obj, e: RoutedEventArgs) =
        if page.NavigationService.CanGoForward then
            page.NavigationService.GoForward()

    static member private GetParentPage(child: DependencyObject) : Page =
        let mutable parent = child
        while not (isNull parent) && not (parent :? Page) do
            parent <- VisualTreeHelper.GetParent(parent)
        parent :?> Page
