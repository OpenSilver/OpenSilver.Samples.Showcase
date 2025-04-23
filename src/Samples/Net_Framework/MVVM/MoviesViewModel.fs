namespace OpenSilver.Samples.Showcase

open System.Collections.ObjectModel
open CommunityToolkit.Mvvm.ComponentModel

type MoviesViewModel() as this =
    inherit ObservableObject()

    let movies = ObservableCollection<Movie>()

    do
        this.AddCommand()

    member this.Movies = movies

    member this.AddCommand() =
        movies.Add(new Movie(Title = "New Movie"))

    member this.RemoveCommand(movie: Movie) =
        movies.Remove(movie) |> ignore
