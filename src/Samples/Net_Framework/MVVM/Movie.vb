Imports System.ComponentModel.DataAnnotations
Imports CommunityToolkit.Mvvm.ComponentModel

Namespace OpenSilver.Samples.Showcase

    Partial Public Class Movie
        Inherits ObservableValidator

        <ObservableProperty>
        <Required(ErrorMessage:="Title is required.")>
        Private _title As String

        <ObservableProperty>
        Private _status As MovieStatus

        <ObservableProperty>
        Private _dateLastWatched As DateTime?

        <ObservableProperty>
        Private _favorite As Boolean

        <ObservableProperty>
        <Range(0.0, 1.0, ErrorMessage:="Rating must be between 0 and 1.")>
        Private _myRating As Double
    End Class

    Public Enum MovieStatus
        ToWatch
        Watching
        Watched
        ToWatchAgain
        Abandoned
    End Enum

End Namespace
