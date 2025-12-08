namespace OpenSilver.Showcase

open System
open System.ComponentModel.DataAnnotations
open CommunityToolkit.Mvvm.ComponentModel

type MovieStatus =
    | ToWatch = 0
    | Watching = 1
    | Watched = 2
    | ToWatchAgain = 3
    | Abandoned = 4

type Movie() =
    inherit ObservableValidator()

    let mutable title = ""
    let mutable status = MovieStatus.ToWatch
    let mutable dateLastWatched = Nullable<DateTime>()
    let mutable favorite = false
    let mutable myRating = 0.0

    [<Required(ErrorMessage = "Title is required.")>]
    member this.Title
        with get() = title
        and set(v) =
            title <- v
            this.OnPropertyChanged("Title")

    member this.Status
        with get() = status
        and set(v) =
            status <- v
            this.OnPropertyChanged("Status")

    member this.DateLastWatched
        with get() = dateLastWatched
        and set(v) =
            dateLastWatched <- v
            this.OnPropertyChanged("DateLastWatched")

    member this.Favorite
        with get() = favorite
        and set(v) =
            favorite <- v
            this.OnPropertyChanged("Favorite")

    [<Range(0.0, 1.0, ErrorMessage = "Rating must be between 0 and 1.")>]
    member this.MyRating
        with get() = myRating
        and set(v) =
            myRating <- v
            this.OnPropertyChanged("MyRating")
