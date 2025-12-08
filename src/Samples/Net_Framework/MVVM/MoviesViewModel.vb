Imports System.Collections.ObjectModel
Imports CommunityToolkit.Mvvm.ComponentModel
Imports CommunityToolkit.Mvvm.Input

Namespace OpenSilver.Samples.Showcase

    Partial Public Class MoviesViewModel
        Inherits ObservableObject

        Public ReadOnly Property Movies As ObservableCollection(Of Movie) = New ObservableCollection(Of Movie)()

        Public Sub New()
            Add()
        End Sub

        <RelayCommand>
        Private Sub Add()
            'Movies.Add(New Movie With {.Title = "New Movie"})
            Movies.Add(New Movie)
        End Sub

        <RelayCommand>
        Private Sub Remove(movie As Movie)
            Movies.Remove(movie)
        End Sub

    End Class

End Namespace
