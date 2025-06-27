namespace OpenSilver.Samples.Showcase

open System.Collections.Generic
open System.Collections.ObjectModel
open System.Linq
open System.Windows.Media

type Pages() =
    static let mutable _allPagesAndCategories : ObservableCollection<PageCategoryInfo> = null
    static let mutable _landingPageInfo : PageInfo = Unchecked.defaultof<PageInfo>
    static let mutable _searchPageInfo : PageInfo = Unchecked.defaultof<PageInfo>

    static member AllPagesAndCategories
        with get() =
            if _allPagesAndCategories = null then
                let brush1 = SolidColorBrush(Color.FromRgb(85uy, 119uy, 240uy))
                let brush2 = SolidColorBrush(Color.FromRgb(205uy, 63uy, 186uy))
                let brush3 = SolidColorBrush(Color.FromRgb(253uy, 163uy, 28uy))
                _allPagesAndCategories <- 
                    ObservableCollection<PageCategoryInfo>(
                        [
                            PageCategoryInfo(
                                Name = "XAML & UI",
                                Foreground = brush1,
                                Pages = ObservableCollection<PageInfo>([
                                    PageInfo(Name = "Controls", Path = "/XAML_Controls", Icon = "\uE913", IconBrush = brush1)
                                    PageInfo(Name = "Data Controls", Path = "/Data_Controls", Icon = "\uF1D0", IconBrush = brush1)
                                    PageInfo(Name = "XAML Features", Path = "/XAML_Features", Icon = "\uE920", IconBrush = brush1)
                                    PageInfo(Name = "Layout", Path = "/XAML_Layout", Icon = "\uE66B", IconBrush = brush1)
                                    PageInfo(Name = "JS Libs", Path = "/JS_Libs", Icon = "\uEB7C", IconBrush = brush1)
                                    PageInfo(Name = "Charts", Path = "/Charts", Icon = "\uE24B", IconBrush = brush1)
                                    PageInfo(Name = "Icons", Path = "/Icons", Icon = "\uE3B6", IconBrush = brush1)
                                ])
                            )
                            PageCategoryInfo(
                                Name = "NON-UI",
                                Foreground = brush2,
                                Pages = ObservableCollection<PageInfo>([
                                    PageInfo(Name = "Client / Server", Path = "/Client_Server", Icon = "\uE1E2", IconBrush = brush2)
                                    PageInfo(Name = ".NET Framework", Path = "/Net_Framework", Icon = "\uE1BD", IconBrush = brush2)
                                    PageInfo(Name = "Native APIs", Path = "/Maui_Hybrid", Icon = "\uE0D4", IconBrush = brush2)
                                ])
                            )
                            PageCategoryInfo(
                                Name = "OTHER",
                                Foreground = brush3,
                                Pages = ObservableCollection<PageInfo>([
                                    PageInfo(Name = "Interop", Path = "/Interop_Samples", Icon = "\uEACD", IconBrush = brush3)
                                    PageInfo(Name = "Performance", Path = "/Performance", Icon = "\uEB9B", IconBrush = brush3)
                                    PageInfo(Name = "Third-Party", Path = "/Third_Party", Icon = "\uEBBB", IconBrush = brush3)
                                    Pages.LandingPageInfo;
                                    Pages.SearchPageInfo
                                ])
                            )
                        ]
                    )
            _allPagesAndCategories

    static member AllPages
        with get() : IEnumerable<PageInfo> =
            Pages.AllPagesAndCategories
            |> Seq.collect (fun c -> if c.Pages <> null then c.Pages :> IEnumerable<PageInfo> else Enumerable.Empty<PageInfo>())

    static member LandingPageInfo
        with get() =
            if obj.ReferenceEquals(_landingPageInfo, null) then
                _landingPageInfo <- new PageInfo(Name = "Home", Path = "/Welcome", IsVisibleInMenu = false)
            _landingPageInfo

    static member SearchPageInfo
        with get() =
            if obj.ReferenceEquals(_searchPageInfo, null) then
                _searchPageInfo <- new PageInfo(Name = "Search", Path = "/Search", IsVisibleInMenu = false)
            _searchPageInfo
