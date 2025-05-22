namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search
open System.Windows
open System.Windows.Browser

[<SearchKeywords("HTML", "browser", "web", "host", "useragent", "platform")>]
type HtmlPage_Demo() as this =
    inherit HtmlPage_DemoXaml()

    do
        this.InitializeComponent()
        this.Loaded.AddHandler(RoutedEventHandler(fun sender args -> this.OnLoaded(sender, args)))

    member private this.OnLoaded(_sender: obj, _e: RoutedEventArgs) =
        this.documentUriTextBlock.Text <- HtmlPage.Document.DocumentUri.OriginalString
