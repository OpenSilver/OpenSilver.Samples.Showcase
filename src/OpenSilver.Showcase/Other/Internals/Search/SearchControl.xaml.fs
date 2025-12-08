namespace OpenSilver.Showcase

open System
open System.Threading.Tasks
open System.Windows
open System.Windows.Input

type SearchControl() as this =
    inherit SearchControlXaml()

    do
        this.InitializeComponent()

        this.SearchField.AddHandler(
            UIElement.KeyDownEvent,
            KeyEventHandler(fun sender e -> this.SearchField_KeyDown(sender, e) |> ignore),
            true
        )

    member this.ButtonSearch_Click(_sender: obj, _e: RoutedEventArgs) =
        async {
            do! this.StartSearch(this.SearchField.Text) |> Async.AwaitTask
            this.SearchField.Focus() |> ignore
        } |> Async.StartAsTask |> ignore

    member private this.SearchField_KeyDown(_sender: obj, e: KeyEventArgs) =
        if e.Key = Key.Enter then
            this.StartSearch(this.SearchField.Text) |> ignore

    member this.StartSearch(searchTerms: string) : Task =
        async {
            //if not (String.IsNullOrWhiteSpace(searchTerms)) then
            //    match Application.Current.RootVisual with
            //    | :? MainPage as mainPage -> 
            //        do! mainPage.StartSearch(searchTerms) |> Async.AwaitTask
            //    | _ -> ()
        } |> Async.StartAsTask :> Task
