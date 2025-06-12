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
                _allPagesAndCategories <- 
                    new ObservableCollection<PageCategoryInfo>([|
                        new PageCategoryInfo(
                            Name = "XAML & UI",
                            Foreground = new SolidColorBrush(Color.FromRgb(85uy, 119uy, 240uy)),
                            Pages = new ObservableCollection<PageInfo>([|
                                new PageInfo(Name = "Controls", Path = "/XAML_Controls", IsVisibleInMenu = true);
                                new PageInfo(Name = "XAML Features", Path = "/XAML_Features", IsVisibleInMenu = true);
                                new PageInfo(Name = "Layout", Path = "/XAML_Layout", IsVisibleInMenu = true);
                                new PageInfo(Name = "JS Libs", Path = "/JS_Libs", IsVisibleInMenu = true);
                                new PageInfo(Name = "Charts", Path = "/Charts", IsVisibleInMenu = true)
                            |])
                        );
                        new PageCategoryInfo(
                            Name = "NON-UI",
                            Foreground = new SolidColorBrush(Color.FromRgb(205uy, 63uy, 186uy)),
                            Pages = new ObservableCollection<PageInfo>([|
                                new PageInfo(Name = "Client / Server", Path = "/Client_Server", IsVisibleInMenu = true);
                                new PageInfo(Name = ".NET Framework", Path = "/Net_Framework", IsVisibleInMenu = true);
                                new PageInfo(Name = "Native APIs", Path = "/Maui_Hybrid", IsVisibleInMenu = true)
                            |])
                        );
                        new PageCategoryInfo(
                            Name = "OTHER",
                            Foreground = new SolidColorBrush(Color.FromRgb(253uy, 163uy, 28uy)),
                            Pages = new ObservableCollection<PageInfo>([|
                                new PageInfo(Name = "Interop", Path = "/Interop_Samples", IsVisibleInMenu = true);
                                new PageInfo(Name = "Performance", Path = "/Performance", IsVisibleInMenu = true);
                                new PageInfo(Name = "Third-Party", Path = "/Third_Party", IsVisibleInMenu = true);
                                Pages.LandingPageInfo;
                                Pages.SearchPageInfo
                            |])
                        )
                    |])
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
