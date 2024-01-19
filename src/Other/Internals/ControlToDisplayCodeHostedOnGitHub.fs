namespace OpenSilver.Samples.Showcase

open System
open System.Windows.Browser
open CSHTML5.Internal
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
#endif

type ControlToDisplayCodeHostedOnGitHub() as this =
    inherit ContentControl()

    let mutable _filePathOnGitHub = ""
    let mutable _displayedHtmlString = ""

    do
        this.Loaded.Add(fun args -> this.OnLoaded(this, args))
        this.VerticalContentAlignment <- VerticalAlignment.Stretch
        this.HorizontalContentAlignment <- HorizontalAlignment.Stretch

    member private this.GetHtmlString(filePath : string) =
        let embedJs = INTERNAL_UriHelper.ConvertToHtml5Path("ms-appx:/Other/embed.js")
        sprintf
            "<script src=\"%s?target=%s&style=github&showBorder=on&showLineNumbers=on&showCopy=on\"></script>"
            embedJs (HttpUtility.UrlEncode("https://github.com" + filePath.Substring(6)))

    member private this.OnLoaded(sender : obj, e : RoutedEventArgs) =
        if not (String.IsNullOrEmpty _filePathOnGitHub) then
            let htmlString = this.GetHtmlString _filePathOnGitHub
            this.DisplayHtmlString(htmlString)

    member private this.DisplayHtmlString(htmlString : string) =
#if SLMIGRATION
        let webView = new WebBrowser()
#else
        let webView = new WebView()
#endif
        webView.NavigateToString(htmlString)
        this.Content <- webView
        _displayedHtmlString <- htmlString

    member this.FilePathOnGitHub
        with get() = _filePathOnGitHub
        and set value =
            _filePathOnGitHub <- value

            if this.IsLoaded then
                let htmlString = this.GetHtmlString this.FilePathOnGitHub
                if htmlString <> _displayedHtmlString then
                    this.DisplayHtmlString(htmlString)
