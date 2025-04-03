namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Input
open OpenSilver.Samples.Showcase
open OpenSilver.Samples.Showcase.Search

type SearchPage() as this =
    inherit SearchPageXaml()

    do
        this.InitializeComponent()
        this.SearchField.AddHandler(
            UIElement.KeyDownEvent,
            KeyEventHandler(fun sender e -> this.SearchField_KeyDown(sender, e)),
            true
        )
        this.SearchField.Loaded.AddHandler(
            RoutedEventHandler(fun sender args ->
                this.OnSearchFieldLoaded(sender, args)
            )
        )


    member private this.OnSearchFieldLoaded(_sender: obj, _e: RoutedEventArgs) =
        this.SearchField.Focus()

        match Application.Current.RootVisual with
        | :? MainPage as mainPage ->
            let originalPath = mainPage.PageContainer.CurrentSource.OriginalString
            if originalPath.Length > 8 then
                let searchTerms = originalPath.Substring(8)
                if not (String.IsNullOrWhiteSpace(searchTerms)) then
                    this.SearchField.Text <- searchTerms
                    this.PerformSearch(searchTerms)
        | _ -> ()

    member private this.ButtonSearch_Click(_sender: obj, _e: RoutedEventArgs) =
        let searchText = this.SearchField.Text
        this.PerformSearch(searchText)

    member private this.SearchField_KeyDown(_sender: obj, e: KeyEventArgs) =
        if e.Key = Key.Enter then
            let searchText = this.SearchField.Text
            this.PerformSearch(searchText)

    member internal this.PerformSearch(searchText: string) =
        this.SamplesContainer.Children.Clear()

        if not (String.IsNullOrWhiteSpace(searchText)) then
            let searchResult = ControlSearch.Search(searchText)

            for res in searchResult do
                let sampleType = SamplesInfoLoader.GetControlTypeByName(res.Name)
                if not (isNull sampleType) then
                    let controlInstance = Activator.CreateInstance(sampleType)
                    match controlInstance with
                    | :? UIElement as uiElement ->
                        this.SamplesContainer.Children.Add(uiElement)
                    | _ -> ()
