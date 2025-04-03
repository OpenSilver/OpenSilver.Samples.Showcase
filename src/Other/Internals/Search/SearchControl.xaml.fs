namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Input

type SearchControl() as this =
    inherit SearchControlXaml()

    do
        this.InitializeComponent()

        this.SearchField.AddHandler(
            UIElement.KeyDownEvent,
            KeyEventHandler(fun sender e -> this.SearchField_KeyDown(sender, e)),
            true
        )

    member this.ButtonSearch_Click(_sender: obj, _e: RoutedEventArgs) =
        this.StartSearch(this.SearchField.Text)

    member private this.SearchField_KeyDown(_sender: obj, e: KeyEventArgs) =
        if e.Key = Key.Enter then
            this.StartSearch(this.SearchField.Text)

    member this.StartSearch(searchTerms: string) =
        if not (String.IsNullOrWhiteSpace(searchTerms)) then
            match Application.Current.RootVisual with
            | :? MainPage as mainPage -> mainPage.StartSearch(searchTerms)
            | _ -> ()
