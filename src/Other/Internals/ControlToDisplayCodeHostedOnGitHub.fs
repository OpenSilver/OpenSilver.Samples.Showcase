namespace OpenSilver.Samples.Showcase

open System.Windows.Browser
open CSHTML5.Internal
open System
open System.Windows
open System.Windows.Controls

type ControlToDisplayCodeHostedOnGitHub() as this =
    inherit ContentControl()

    let mutable _filePathOnGitHub = ""
    let mutable _displayedHtmlString = ""

    do
        this.Loaded.Add(fun args -> this.OnLoaded(this, args))
        this.VerticalContentAlignment <- VerticalAlignment.Stretch
        this.HorizontalContentAlignment <- HorizontalAlignment.Stretch

    public new(absolutePath: string) as this =
        ControlToDisplayCodeHostedOnGitHub()
        then
            this.FilePathOnGitHub <- absolutePath

    member private this.GetHtmlString(filePath : string) =
        let embedJs = INTERNAL_UriHelper.ConvertToHtml5Path("ms-appx:/Other/embed.js")
        let themeMode =
            match Application.Current.Theme with
            | :? OpenSilver.Themes.Modern.ModernTheme as theme when theme.CurrentPalette = OpenSilver.Themes.Modern.ModernTheme.Palettes.Dark ->
                "dark"
            | _ -> "github"
        sprintf
            "<script src=\"%s?target=%s&style=%s&showBorder=on&showLineNumbers=on&showCopy=on\"></script>"
            embedJs (HttpUtility.UrlEncode(filePath)) themeMode

    member private this.OnLoaded(sender : obj, e : RoutedEventArgs) =
        if not (String.IsNullOrEmpty _filePathOnGitHub) then
            let htmlString = this.GetHtmlString _filePathOnGitHub
            this.DisplayHtmlString(htmlString)

    member private this.DisplayHtmlString(htmlString : string) =
        let webView = new WebBrowser()
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

    member this.Refresh() =
        if not (String.IsNullOrEmpty(_filePathOnGitHub)) && this.IsLoaded then
            let htmlString = this.GetHtmlString(_filePathOnGitHub)
            this.DisplayHtmlString(htmlString)
