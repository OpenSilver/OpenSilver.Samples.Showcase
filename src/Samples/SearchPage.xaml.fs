namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Input
open System.Windows.Navigation
open OpenSilver.Samples.Showcase.Search

type SearchPage() as this =
    inherit SearchPageXaml()

    let [<Literal>] searchArgName = "SearchTerms"

    do
        this.InitializeComponent()

        //this.SearchField.Loaded.AddHandler(
        //    RoutedEventHandler(fun sender args ->
        //        this.OnSearchFieldLoaded(sender, args)
        //    )
        //)

        //this.SearchField.AddHandler(
        //    UIElement.KeyDownEvent,
        //    KeyEventHandler(fun sender e -> this.SearchField_KeyDown(sender, e)),
        //    true
        //)

    override this.OnNavigatedTo(e: NavigationEventArgs) =
        let found, searchTerms = this.NavigationContext.QueryString.TryGetValue(searchArgName)
        if found && not (String.IsNullOrWhiteSpace searchTerms) then
            //this.SearchField.Text <- searchTerms
            this.PerformSearch(searchTerms)

    //member private this.OnSearchFieldLoaded(_sender: obj, _e: RoutedEventArgs) =
    //    this.SearchField.Focus() |> ignore

    //member private this.ButtonSearch_Click(_sender: obj, _e: RoutedEventArgs) =
    //    this.NavigateToSearch()

    //member private this.SearchField_KeyDown(_sender: obj, e: KeyEventArgs) =
    //    if e.Key = Key.Enter then
    //        this.NavigateToSearch()

    //member private this.NavigateToSearch() =
    //    let searchText = this.SearchField.Text
    //    this.NavigationService.Navigate(Uri($"/Search/{searchText}", UriKind.Relative)) |> ignore

    member internal this.PerformSearch(searchText: string) =
        this.SamplesContainer.Children.Clear()

        if not (String.IsNullOrWhiteSpace searchText) then
            let searchResult = ControlSearch.Search(searchText)

            for res in searchResult do
                let sampleType = SamplesInfoLoader.GetControlTypeByName(res.Name)
                if not (isNull sampleType) then
                    let controlInstance = Activator.CreateInstance(sampleType)
                    match controlInstance with
                    | :? UIElement as uiElement ->
                        this.SamplesContainer.Children.Add(uiElement)
                    | _ -> ()
