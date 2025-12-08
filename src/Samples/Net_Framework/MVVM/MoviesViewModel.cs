using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace OpenSilver.Samples.Showcase;

public partial class MoviesViewModel : ObservableObject
{
    public ObservableCollection<Movie> Movies { get; } = [];

    public MoviesViewModel()
    {
        Add();
    }

    [RelayCommand]
    private void Add()
    {
        Movies.Add(new Movie { Title = "New Movie" });
    }

    [RelayCommand]
    private void Remove(Movie movie)
    {
        Movies.Remove(movie);
    }
}
