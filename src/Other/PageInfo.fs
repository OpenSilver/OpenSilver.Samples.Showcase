namespace OpenSilver.Samples.Showcase

[<AllowNullLiteral>]
type PageInfo(name: string, path: string) =

    // Static mutable backing fields
    static let mutable pageInfos: PageInfo list option = None
    static let mutable landingPageInfo: PageInfo option = None
    static let mutable searchPageInfo: PageInfo option = None

    // Auto-properties with mutable backing, needed for data binding
    member val Name = name with get, set
    member val Path = path with get, set


    static member Pages =
        match pageInfos with
        | Some list -> list
        | None ->
            let pages = [
                PageInfo("Panels & Controls", "/XAML_Controls")
                PageInfo("Xaml Features", "/XAML_Features")
                PageInfo(".NET Framework", "/Net_Framework")
                PageInfo("Client / Server", "/Client_Server")
                PageInfo("Interop", "/Interop_Samples")
                PageInfo("Charts", "/Charts")
                PageInfo("Performance", "/Performance")
                PageInfo("Native APIs", "/Maui_Hybrid")
                PageInfo("Third-Party", "/Third_Party")
                PageInfo.LandingPageInfo
                PageInfo.SearchPageInfo
            ]
            pageInfos <- Some pages
            pages

    static member LandingPageInfo =
        match landingPageInfo with
        | Some page -> page
        | None ->
            let page = PageInfo("Home", "/Welcome")
            landingPageInfo <- Some page
            page

    static member SearchPageInfo =
        match searchPageInfo with
        | Some page -> page
        | None ->
            let page = PageInfo("Search", "/Search")
            searchPageInfo <- Some page
            page
